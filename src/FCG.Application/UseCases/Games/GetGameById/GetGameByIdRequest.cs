namespace FCG.Application.UseCases.Games.GetGameById;

public class GetGameByIdRequest
{
    public Guid GameId { get; init; }

    public GetGameByIdRequest(Guid gameId)
    {
        GameId = gameId;
    }
}