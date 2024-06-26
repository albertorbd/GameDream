﻿

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

        if(!string.IsNullOrEmpty(newEmail) && IsEmailTaken(newEmail)){
            Console.WriteLine("El correo está siendo utilizado por otro usuario");
            return;
        }
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

    public void BuyVideogame(User user, Videogame videogame, string concept)
{
    try
    {
        if (user.Money >= videogame.Price)
        {
            if (!user.Videogames.ContainsKey(videogame.Name))
            {
                user.Money -= videogame.Price.Value; // Solo se descuenta si el usuario no posee el videojuego
                IdTransationAument(user);

                user.Videogames.Add(videogame.Name, videogame.Price.Value);
                Console.WriteLine($"Has comprado el videojuego {videogame.Name}.");

                Operation operation = new Operation(videogame, $"Comprar {videogame.Name}", videogame.Price.Value);
                user.Operations.Add(operation);
                _repository.UpdateUser(user);
                _repository.SaveChanges();
            }
            else
            {
                Console.WriteLine("Ya posees este videojuego.");
            }
        }
        else
        {
            Console.WriteLine("No tienes suficiente saldo para comprar este videojuego.");
        }
    }
    catch (Exception e)
    {
        _repository.LogError("Error buying the videogame", e);
        throw new Exception("An error has occurred buying the videogame");
    }
}
    public void PrintVideogameBought(User user)
    {
        try
        {  
            
            Console.WriteLine("Lista de videojuegos en posesión:\n");

            foreach (var videogame in user.Videogames)
            {
                
                Console.WriteLine($"{videogame.Key}: {videogame.Value}\n");
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error trying to show your videogames", e);
            throw new Exception("An error has ocurred trying to show your videogames", e);
        }
    }
     public void PrintOperations(User user)
    {
        try
        {
            List<Operation> allOperations = user.Operations;
            Console.WriteLine("Lista de Operaciones:\n");
            foreach (var operation in allOperations)
            {
                if (operation.Videogame == null)
                {
                    Console.WriteLine($"ID: {operation.Id}, Concepto: {operation.Concept}, Fecha: {operation.Date}, Cantidad: {operation.Amount}, Método de pago: {operation.Method}\n");
                }
                else
                {
                    Console.WriteLine($"ID: {operation.Id}, Videojuego: {operation.Videogame.Name}, Precio: {operation.Videogame.Price}, Concepto: {operation.Concept}, Fecha: {operation.Date}\n");

                }
            }
        }
        catch (Exception e)
        {
            _repository.LogError("Error al obtener las transacciones", e);
            throw new Exception("Ha ocurrido un error al obtener las transacciones", e);
        }
    }


    public bool IsEmailTaken(string email){
        try{
            var users= _repository.GetAllUsers().Values;
            foreach(var user in users){
                if(user.Email==email){
                    return true;
                }
            }
            return false;
        }catch (Exception e)
        {
            _repository.LogError("Error checking email", e);
            throw new Exception("An error has ocurred checking if email is in use", e);
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
