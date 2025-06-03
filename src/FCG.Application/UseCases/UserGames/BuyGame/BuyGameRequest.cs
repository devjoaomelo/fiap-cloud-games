using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.UserGames.BuyGame;

public class BuyGameRequest
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid GameId { get; set; }

    public BuyGameRequest(Guid userId, Guid gameId)
    {
        UserId = userId;
        GameId = gameId;
    }
}