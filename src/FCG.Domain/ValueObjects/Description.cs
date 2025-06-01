namespace FCG.Domain.ValueObjects;

public sealed class Description
{
    public string Text { get; }

    public Description(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Description cannot be empty");
        }
        Text = text;
    }
    
    public override bool Equals(object? obj) => obj is Description description && Text == description.Text;
    
    public override int GetHashCode() => Text.GetHashCode();
    
    public static implicit operator Description(string text) => new Description(text);
}