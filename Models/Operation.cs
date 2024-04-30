namespace Gamedream.Models;
public class Operation
{
    public int Id { get; set; }
    public Videogame Videogame { get; set; }
    public string? Concept { get; set; }
    public DateTime Date { get; set; }
    public double Quantity { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }
    public string? Method { get; set; }

    public static int IdTransactionSeed;

    public Operation() {}


     public Operation (string concept, double amount, string method)
    {
        Id = IdTransactionSeed++;
        Videogame = null;
        Concept = concept;
        Date = DateTime.Now;
        Amount = amount;
        Method = method;
    }

    public Operation (Videogame videogame, string concept, double price, double quantity, double amount, string method)
    {
        Id = IdTransactionSeed++;
        Videogame = videogame;
        Concept = concept;
        Date = DateTime.Now;
        Price = price;
        Quantity = quantity; 
        Amount=amount;
        Method = method;
    }

}