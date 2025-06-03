using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.Games.UpdateGame;

public class UpdateGameRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}