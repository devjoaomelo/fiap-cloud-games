namespace FCG.Application.UseCases.UserGames.RemoveGameFromUser;

public class RemoveGameFromUserResponse
{
    public bool IsDeleted { get; init; }
    public string Message { get; init; }

    public RemoveGameFromUserResponse(bool isDeleted, string message)
    {
        IsDeleted = isDeleted;
        Message = message;
    }
}