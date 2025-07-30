using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.Games.CreateGame;

public class CreateGameRequest
{
    [Required, MinLength(2)]
    public string Title { get; init; }

    [Required, MinLength(5)]
    public string Description { get; init; }

    [Required, Range(0.01, 9999.99)]
    public decimal Price { get; init; }

    public CreateGameRequest(decimal price, string title, string description)
    {
        Title = title;
        Description = description;
        Price = price;
    }
}