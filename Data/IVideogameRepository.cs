using Gamedream.Models;

namespace Gamedream.Data;

public interface IVideogameRepository
{
    void AddVideogame(Videogame videogame);
    Dictionary<string,Videogame> GetAllVideogames();
    Videogame GetVideogame(string name);
    void DeleteVideogame(Videogame videogame);
    void UpdateVideogame(Videogame videogame);
    void SaveChanges();
    void LogError(string message, Exception exception);


}