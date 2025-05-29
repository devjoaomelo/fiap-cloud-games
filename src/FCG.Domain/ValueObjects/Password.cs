using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects;
public class Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValid(value))
            throw new ArgumentException("Invalid password. Password must be at least 8 characters long, including letters, numbers and special characters.");
        Value = value;
    }

    private bool IsValid(string password)
    {
        // Minimum 8 characters, 1 letter, 1 number, 1 special character
        return Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$");
    }

    public override string ToString() => Value;

    public override bool Equals(object obj)
    {
        if (obj is not Password other) return false;
        return Value == other.Value;
    }
    public override int GetHashCode() => Value.GetHashCode();
}

