using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.UserGames.RemoveGameFromUser;

public class RemoveGameFromUserRequest
{
    [Required]
    public Guid UserId { get; }
    [Required]
    public Guid GameId { get; }

    public RemoveGameFromUserRequest(Guid userId, Guid gameId)
    {
        UserId = userId;
        GameId = gameId;
    }
}