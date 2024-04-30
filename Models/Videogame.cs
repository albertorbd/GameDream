namespace Gamedream.Models;
public class Videogame
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public DateTime RegisterDate { get; set; }
    public string? Developer { get; set; }
     public string? Platform { get; set; }

    public int? Valoration { get; set; }
    
    
    public static int VideogameIdSeed { get; set; }

    public Videogame() {}

    public Videogame (string name, string genre, string description, string developer, string platform, int valoration){

    Id = VideogameIdSeed++;
    Name = name;
    Genre = genre;
    Description = description;
    RegisterDate= DateTime.Now;
    Developer = developer;
    Platform= platform;
    Valoration = valoration;
    }
}
