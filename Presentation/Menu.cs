using System.Collections;
using System.Runtime.CompilerServices;
using Gamedream.Business;
using Gamedream.Models;


using System;

namespace Gamedream.Presentation
{
    public class Menu
    {
        private readonly IVideogameService _videogameService;

        public Menu(IVideogameService videogameService)
        {
            _videogameService = videogameService;
        }

        public void CreateVideogame()
        {
            Console.WriteLine("Crear un nuevo videojuego:");
            Console.Write("Nombre: ");
            string name = _videogameService.InputEmpty();
            Console.Write("Género: ");
            string genre = _videogameService.InputEmpty();
            Console.Write("Descripción: ");
            string description = _videogameService.InputEmpty();
            Console.WriteLine("Precio ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Introduce un valor numérico válido: ");
            }

            Console.Write("Desarrollador: ");
            string developer = _videogameService.InputEmpty();
            Console.Write("Plataforma: ");
            string platform = _videogameService.InputEmpty();
            Console.Write("Valoración: ");
            int valoration;
            while (!int.TryParse(Console.ReadLine(), out valoration))
            {
                Console.WriteLine("Introduce un valor numérico válido: ");
            }

            try
            {
                _videogameService.RegisterVideogame(name, genre, description,price, developer, platform, valoration);
                Console.WriteLine("Videojuego creado con éxito.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al crear el videojuego: {e.Message}");
            }
        }

        public void ShowAllVideogames(){

        try{
         _videogameService.PrintAllVideogames();
        }catch(Exception e){
            Console.WriteLine($"Error al mostrar los videojuegos: {e.Message}");
        }
        }
        public void DeleteVideogame()
        {
            try
            {
                Console.WriteLine("Ingrese el nombre del videojuego que desea eliminar:");
                string name = _videogameService.InputEmpty();

                _videogameService.DeleteVideogame(name);
                Console.WriteLine("El videojuego se ha eliminado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al eliminar el videojuego: {e.Message}");
            }
        }

        public void UpdateVideogame()
{
    try
    {
        Console.WriteLine("Ingrese el nombre del videojuego que desea actualizar:");
        string videogameName = _videogameService.InputEmpty();

        // Obtener el videojuego por su nombre
        Videogame videogameToUpdate = _videogameService.GetVideogame(videogameName);

        if (videogameToUpdate != null)
        {
            Console.WriteLine($"Ingrese el nuevo género para {videogameName}:");
            string newGenre = _videogameService.InputEmpty();

            Console.WriteLine($"Ingrese la nueva descripción para {videogameName}:");
            string newDescription = _videogameService.InputEmpty();

            Console.WriteLine($"Ingrese el nuevo desarrollador para {videogameName}:");
            string newDeveloper = _videogameService.InputEmpty();

            Console.WriteLine($"Ingrese la nueva plataforma para {videogameName}:");
            string newPlatform = _videogameService.InputEmpty();

            Console.WriteLine($"Ingrese la nueva valoración para {videogameName}:");
            int newValoration = Convert.ToInt32(Console.ReadLine());

            // Actualizar el videojuego
            _videogameService.UpdateVideogame(videogameToUpdate, newGenre, newDescription, newDeveloper, newPlatform, newValoration);
            Console.WriteLine("El videojuego se ha actualizado correctamente.");
        }
        else
        {
            Console.WriteLine("No se encontró ningún videojuego con ese nombre.");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error al actualizar el videojuego: {e.Message}");
    }
}

    }




    }

    

