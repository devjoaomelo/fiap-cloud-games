namespace FCG.Domain.ValueObjects;

public sealed class Price
{
    public decimal Value { get; }

    public Price(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        
        Value = value;
    }
    
    public override bool Equals(object? obj) => obj is Price price && Value == price.Value;
    
    public override int GetHashCode() => Value.GetHashCode();
    
    public static implicit operator decimal(Price price) => price.Value;
    public static implicit operator Price(decimal value) => new Price(value);
    
}