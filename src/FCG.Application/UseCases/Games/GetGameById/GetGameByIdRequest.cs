namespace FCG.Application.UseCases.Games.GetGameById;

public class GetGameByIdRequest
{
    public Guid GameId { get; set; }

    public GetGameByIdRequest(Guid gameId)
    {
        GameId = gameId;
    }
}