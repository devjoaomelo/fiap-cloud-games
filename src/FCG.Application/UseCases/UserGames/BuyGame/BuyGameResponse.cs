namespace FCG.Application.UseCases.UserGames.BuyGame;

public class BuyGameResponse
{
    public Guid UserGameId { get; set; }

    public BuyGameResponse(Guid userGameId)
    {
        UserGameId = userGameId;
    }
}