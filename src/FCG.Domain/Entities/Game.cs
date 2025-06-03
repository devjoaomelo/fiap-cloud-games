using System.ComponentModel.DataAnnotations;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities;

public sealed class Game
{
    public Guid Id { get; private set; }
    [Required]
    public Title Title { get; private set; }
    [Required]
    public Description Description { get; private set; }
    [Required]
    public Price Price { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public ICollection<UserGame> UserGames { get; private set; } = new List<UserGame>();

    public Game(Title title, Description description, Price price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Price = price;
        CreatedDate = DateTime.UtcNow;
    }
    
    private Game() { }

    public void Update(Title title, Description description, Price newPrice)
    {
        Title = title;
        Description = description;
        Price = newPrice;
    }
}