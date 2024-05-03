using Gamedream.Business;
using Gamedream.Models;

namespace Gamedream.Presentation;

public class privateMenu
{
    public readonly IUserService _userService;
    public readonly IVideogameService _videogameService;
    private User currentUser;

    public privateMenu(IUserService userService, IVideogameService videogameService)
    {
        _userService = userService;
        _videogameService = videogameService;
    }

    public void MainPrivateMenu(string userEmail)
    {
        currentUser = _userService.GetUser(userEmail);

        Console.WriteLine("\n ----------Gamedream------------------\n");
        Console.WriteLine($" Efectivo: {currentUser.Money} €\n");
        Console.WriteLine($"Nombre: {currentUser.Name}\n");
        Console.WriteLine($"Apellidos: {currentUser.Lastname}\n");
        Console.WriteLine($"Correo: {currentUser.Email}\n");
        Console.WriteLine($"Contraseña: {currentUser.Password}\n");
        Console.WriteLine($"Nombre: {currentUser.Name}\n");
        Console.WriteLine($"Fecha de nacimiento: {currentUser.BirthDate}\n");
        Console.WriteLine($"DNI: {currentUser.DNI}\n");

        Console.WriteLine("1. Modificar correo | 2. Modificar contraseña");
        Console.WriteLine("3. Eliminar cuenta");
        Console.WriteLine("4. Volver");
    }

    }