namespace FCG.Application.UseCases.Games.CreateGame;

public class CreateGameRequest
{
    public string Title {get; init;}
    public string Description {get; init;}
    public decimal Price {get; init;}

    public CreateGameRequest(decimal price, string title, string description)
    {
        Title = title;
        Description = description;
        Price = price;
    }
}