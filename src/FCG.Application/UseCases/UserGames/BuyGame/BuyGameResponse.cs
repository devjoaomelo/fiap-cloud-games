namespace FCG.Application.UseCases.UserGames.BuyGame;

public class BuyGameResponse
{
    public bool IsSuccess { get; }
    public string Message { get; }

    public BuyGameResponse(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}