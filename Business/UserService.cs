using System.Security.Cryptography;
using Microsoft.VisualBasic;

using Gamedream.Data;
using Gamedream.Models;

namespace Gamedream.Business;
public class UserService : IUserService
{

private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

public void RegisterUser(string name, string lastname, string email, string password, string dni, DateTime birthdate){
try
 {
    User user= new(name,lastname, email, password, dni, birthdate);
    _repository.AddUser(user);
    _repository.SaveChanges();
 }   
 catch(Exception e){
    _repository.LogError("Error registering the user", e);
    throw new Exception("An error ocurred registering the user", e);
 }    
}

public void PrintAllUsers(){
    try
    {
    Dictionary<string, User> users= _repository.GetAllUsers();
    Console.WriteLine("Lista de usuarios: \n");
    foreach (var user in users.Values){
    Console.WriteLine($"ID: {user.Id}, Nombre: {user.Name}, Apellidos: {user.Lastname}, Email: {user.Email}, Contraseña: {user.Password}, DNI: {user.DNI}, Fecha de nacimiento:{user.BirthDate}");
        }
    }catch(Exception e )
    {
        _repository.LogError("Error printing the users", e);
        throw new Exception("An error has ocurred printing the users", e);
    }
}

public bool CheckRepeatUser(string dni, string email){
try
    {
    foreach (var user in _repository.GetAllUsers().Values)
     {
        if (user.DNI.Equals(dni, StringComparison.OrdinalIgnoreCase) || 
        user.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
         {
          return true;
         }
        }

            return false;
        }
        catch (Exception e)
        {
            _repository.LogError("Error checking user", e);
            throw new Exception("An error has ocurred checking user", e);
        }
}

public User GetUser(string email){
        try{
            return _repository.GetUser(email);
        }
        catch(Exception e){
            _repository.LogError("Error getting the user", e);
            throw new Exception("An error has ocurred getting the user", e);
        }
    }

public void DeleteUser(string userEmail){
         
        try{
          User getUser = GetUser(userEmail);

           if (getUser != null){
           _repository.DeleteUser(getUser);
           _repository.SaveChanges();
           Console.WriteLine("User has been removed");
           }else{
            Console.WriteLine("No user found");
           }
        }catch(Exception e ){
            _repository.LogError("Error deleting user", e);
            throw new Exception("An error ocurred deleting the user", e);
        }
    }


  public void UpdateUser(string userEmail, string  newEmail= null, string newPassword=null){
        try{
        User userUpdated= _repository.GetUser(userEmail);
         if (!string.IsNullOrEmpty(newEmail))
            {
                userUpdated.Email = newEmail;
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                userUpdated.Password = newPassword;
            }

        _repository.UpdateUser(userUpdated);
        _repository.SaveChanges();
        }catch(Exception e){
            _repository.LogError("Error updating user",e);
            throw new Exception("An error has ocurred updating user");

        }
    }  


    public void Deposit(User user, string concept, string toDeposit, string method){
        try{

            if(double.TryParse(toDeposit, out double amount)){
            IdTransationAument(user);

            user.Money+=amount;
            Operation operation=new Operation(concept,amount,method);
            user.Operations.Add(operation);
            _repository.UpdateUser(user);
            _repository.SaveChanges();
            Console.WriteLine($"Depósito de {operation.Amount}€ exitoso!!");
        }else{
            Console.WriteLine("Introduce un número por favor");
        };
        }
         catch (Exception e)
        {
            _repository.LogError("Error trying to deposit money", e);
            throw new Exception("An error has ocurred tryng to deposit money", e);
        }
    }

    public void Withdrawal(User user, string concept, string toWithdraw, string method){
        try{

            if(double.TryParse(toWithdraw, out double amount)){

                if (user.Money < amount) 
                {
                    Console.WriteLine("No tienes suficiente dinero ");
                    return;
                }
            IdTransationAument(user);
             Operation operation=new Operation(concept,amount,method);
            user.Operations.Add(operation);
            user.Money-=operation.Amount;
            _repository.UpdateUser(user);
            _repository.SaveChanges();
            Console.WriteLine($"retirada de {operation.Amount}€ exitoso!!");
        }else{
            Console.WriteLine("Introduce un número por favor");
        };
        }
         catch (Exception e)
        {
            _repository.LogError("Error trying to withdraw money", e);
            throw new Exception("An error has ocurred tryng to withdraw money", e);
        }
    }

    public void BuyVideogame(User user, Videogame videogame,string concept){
    try{
       if (user.Money>=videogame.Price){
    IdTransationAument(user);
    if(videogame.Price.HasValue){  
     user.Money-=videogame.Price.Value;
}
    if (!user.Videogames.ContainsKey(videogame.Name))
        {
            user.Videogames.Add(videogame.Name, videogame.Price.Value);
        }else
        {
            throw new InvalidOperationException("Ya posees este videojuego.");
        }
        Operation operation = new Operation(videogame, $"Comprar {videogame.Name}", videogame.Price.Value);
        user.Operations.Add(operation);
        _repository.UpdateUser(user);
        _repository.SaveChanges();

       }else
    {
        throw new InvalidOperationException("No tienes suficiente saldo para comprar este videojuego.");
    }
    }catch(Exception e)
    {
        _repository.LogError("Error buying the videogame", e);
        throw new Exception("An error has ocurred buying the videogame");
    }
    }
    private void IdTransationAument(User user)
    {
        try
        {
                if (user.Operations.Count == 0)
                {
                    Operation.IdOperationSeed = 1;
                }
                else
                {
                    Operation.IdOperationSeed = user.Operations.Count + 1;
                }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al asignar el ID", e);
            throw new Exception("Ha ocurrido un error al asignar el ID", e);
        }
    }
     public string InputEmpty()
    {
        try
        {
            string input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("El campo está vacío.");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }
        catch (Exception e)
        {
            _repository.LogError("Error al comprobar el campo", e);
            throw new Exception("Ha ocurrido un error al comprobar el campo", e);
        }
    }
public bool loginCheck(string email, string password){
try{
   foreach(var user in _repository.GetAllUsers().Values){
    if((user.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
     user.Password.Equals(password))){
        return true;
     }
   } 
   return false;
}
catch (Exception e)
        {
            _repository.LogError("error checking user", e);
            throw new Exception("An error has ocurred checking user", e);
        }
}

}
