namespace FCG.Domain.ValueObjects;

public sealed class Title
{
    public string Name { get; }

    public Title(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException("Title cannot be empty");
        }
        Name = name;
    }

    public override bool Equals(object? obj) => obj is Title title && Name == title.Name;
    
    public override int GetHashCode() => Name.GetHashCode();
    
    public static implicit operator string(Title title) => title.Name;
    
    
}