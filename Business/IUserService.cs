using System.Reflection.PortableExecutable;
using Gamedream.Models;
namespace Gamedream.Business;

public interface IUserService
{
void RegisterUser(string name, string lastname, string email, string password, string dni, DateTime birthdate);
void PrintAllUsers();  
bool CheckRepeatUser(string email, string dni);
User GetUser(string email);
void DeleteUser(string email);
void UpdateUser(string email, string newEmail=null, string newPassword= null);
void Deposit(User user, string concept, string amount, string method);
void Withdrawal(User user, string concept, string amount, string method);
void BuyVideogame(User user, Videogame videogame, string concept);
string InputEmpty();
bool loginCheck(string email, string password);

}