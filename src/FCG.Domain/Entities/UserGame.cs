using System.ComponentModel.DataAnnotations;

namespace FCG.Domain.Entities;

public class UserGame
{
    public Guid Id { get; private set; }
    [Required]
    public Guid UserId { get; private set; }
    [Required]
    public User User { get; private set; }
    [Required]
    public Guid GameId { get; private set; }
    [Required]
    public Game Game { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    
    private UserGame(){}

    public UserGame(Guid userId, Guid gameId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        GameId = gameId;
        PurchaseDate = DateTime.UtcNow;
    }
    
}