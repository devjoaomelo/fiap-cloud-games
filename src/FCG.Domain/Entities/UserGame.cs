namespace FCG.Domain.Entities;

public class UserGame
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public Guid GameId { get; private set; }
    public Game Game { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    
    private UserGame(){}

    public UserGame(Guid userId, Guid gameId)
    {
        if(userId == Guid.Empty) throw new ArgumentException("UserId must be a valid GUID", nameof(userId));
        if(gameId == Guid.Empty) throw new ArgumentException("GameId must be a valid GUID", nameof(gameId));
        
        Id = Guid.NewGuid();
        UserId = userId;
        GameId = gameId;
        PurchaseDate = DateTime.UtcNow;
    }
    
}