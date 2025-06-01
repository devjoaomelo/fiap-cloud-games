using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities;

public sealed class Game
{
    public Guid GameId { get; private set; }
    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public Price Price { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public ICollection<UserGame> UsersGames { get; private set; } = new List<UserGame>();

    public Game(Title title, Description description, Price price)
    {
        GameId = Guid.NewGuid();
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