using Gamedream.Business;
using Gamedream.Data;
namespace Gamedream.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            IVideogameService videogameService = new VideogameService(new VideogameRepository());
            Menu menu = new Menu(videogameService);
             menu.CreateVideogame();
            
           
           
          
             
            
        }
    }
}
