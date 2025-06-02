namespace FCG.Application.UseCases.Games.GetGameById;

public class GetGameByIdResponse
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
    public DateTime CreatedDate { get; }

    public GetGameByIdResponse(Guid id, string name, string description, decimal price, DateTime createdDate)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        CreatedDate = createdDate;
        
    }
    
}