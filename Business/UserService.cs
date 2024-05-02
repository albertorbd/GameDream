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
            _repository.LogError("Error updating videogame",e);
            throw new Exception("An error has ocurred updating videogame");

        }
    }  
}
