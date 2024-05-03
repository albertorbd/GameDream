using Gamedream.Models;

namespace Gamedream.Data;

public interface IUserRepository{
void AddUser(User user); 
 Dictionary<string,User> GetAllUsers();
 User GetUser(string email);
 void DeleteUser(User user);
 void UpdateUser(User user);
 void SaveChanges();
 void LogError(string message, Exception exception);

}