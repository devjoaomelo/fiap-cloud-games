namespace FCG.Application.UseCases.UserGames.BuyGame;

public class BuyGameRequest
{
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }

    public BuyGameRequest(Guid userId, Guid gameId)
    {
        UserId = userId;
        GameId = gameId;
    }
}