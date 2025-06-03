namespace FCG.Application.UseCases.Games.GetAllGames;

public class GetAllGamesResponse
{
    public Guid Id { get; }
    public string Title { get; }
    public string Description { get; }
    public decimal Price { get; }
    public DateTime CreateDate { get; }

    public GetAllGamesResponse(Guid id, string title, string description, decimal price, DateTime createDate)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        CreateDate = createDate;
    }
}