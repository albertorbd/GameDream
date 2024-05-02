using Gamedream.Models;

namespace Gamedream.Data;

public interface IUserRepository{
void AddUser(User user); 
 Dictionary<string,User> GetAllUsers();
 User GetUser(string email);
 void LogError(string message, Exception exception);

}