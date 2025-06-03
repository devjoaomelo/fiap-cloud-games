using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.Games.CreateGame;

public class CreateGameRequest
{
    [Required]
    public string Title {get; init;}
    [Required]
    public string Description {get; init;}
    [Required]
    public decimal Price {get; init;}

    public CreateGameRequest(decimal price, string title, string description)
    {
        Title = title;
        Description = description;
        Price = price;
    }
}