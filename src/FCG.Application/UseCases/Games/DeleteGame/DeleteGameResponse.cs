namespace FCG.Application.UseCases.Games.DeleteGame;

public class DeleteGameResponse
{
    public bool IsDeleted { get; init; }
    public string Message { get; init; }

    public DeleteGameResponse(bool isDeleted, string message)
    {
        IsDeleted = isDeleted;
        Message = message;
    }
}