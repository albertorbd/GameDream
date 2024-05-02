using System.Reflection.PortableExecutable;
using Gamedream.Models;
namespace Gamedream.Business;

public interface IUserService
{
void RegisterUser(string name, string lastname, string email, string password, string dni, DateTime birthdate);
    
}