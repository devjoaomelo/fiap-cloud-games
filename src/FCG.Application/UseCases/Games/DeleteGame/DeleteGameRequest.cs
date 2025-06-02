namespace FCG.Application.UseCases.Games.DeleteGame;

public class DeleteGameRequest
{
    public Guid Id { get; set; }

    public DeleteGameRequest(Guid id)
    {
        Id = id;
    }
}