using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities;

public sealed class Game
{
    public Guid Id { get; private set; }
    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public Price Price { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public ICollection<UserGame> UserGames { get; private set; } = new List<UserGame>();

    public Game(Title title, Description description, Price price)
    {
        if (title is null) throw new ArgumentNullException(nameof(title));
        if (description is null) throw new ArgumentNullException(nameof(description));
        if (price is null) throw new ArgumentNullException(nameof(price));
        
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Price = price;
        CreatedDate = DateTime.Now;
    }
    
    private Game() { }

    public void Update(Title title, Description description, Price newPrice)
    {
        if (title is null) throw new ArgumentNullException(nameof(title));
        if (description is null) throw new ArgumentNullException(nameof(description));
        if (newPrice is null) throw new ArgumentNullException(nameof(newPrice));
        
        Title = title;
        Description = description;
        Price = newPrice;
    }
}