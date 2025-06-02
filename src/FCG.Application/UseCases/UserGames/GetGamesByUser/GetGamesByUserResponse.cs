namespace FCG.Application.UseCases.UserGames.GetGamesByUser;

public class GetGamesByUserResponse
{
    public Guid GameId { get; }
    public string Title { get; }
    public string Description { get; }
    public decimal Price { get; }
    public DateTime PurchaseDate { get; }

    public GetGamesByUserResponse(Guid gameId, string title, string description, decimal price, DateTime purchaseDate)
    {
        GameId = gameId;
        Title = title;
        Description = description;
        Price = price;
        PurchaseDate = purchaseDate;
    }
}