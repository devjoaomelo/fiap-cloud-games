namespace FCG.Application.UseCases.Games.CreateGame;

public class CreateGameResponse
{
    public Guid Id { get; }

    public CreateGameResponse(Guid id)
    {
        Id = id;
    }
}