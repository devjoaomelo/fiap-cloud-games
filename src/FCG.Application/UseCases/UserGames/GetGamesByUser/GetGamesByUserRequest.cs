namespace FCG.Application.UseCases.UserGames.GetGamesByUser;

public class GetGamesByUserRequest
{
    public Guid UserId { get; }

    public GetGamesByUserRequest(Guid userId)
    {
        UserId = userId;
    }
}