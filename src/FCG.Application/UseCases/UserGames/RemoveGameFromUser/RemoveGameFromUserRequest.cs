namespace FCG.Application.UseCases.UserGames.RemoveGameFromUser;

public class RemoveGameFromUserRequest
{
    public Guid UserId { get; }
    public Guid GameId { get; }

    public RemoveGameFromUserRequest(Guid userId, Guid gameId)
    {
        UserId = userId;
        GameId = gameId;
    }
}