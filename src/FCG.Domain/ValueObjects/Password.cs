using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects;
public class Password
{
    public string Hash { get; private set; }
    
    protected Password()
    {
        Hash = null!;
    }

    public Password(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || !IsValid(password))
        {
            throw new ArgumentException("Invalid password. Password must be at least 8 characters long, including letters, numbers and special characters.");
        }
        Hash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    private static bool IsValid(string password)
    {
        // Minimum 8 characters, 1 letter, 1 number, 1 special character
        return !string.IsNullOrWhiteSpace(password) && Regex.IsMatch(password, @"^(?=.*\p{Lu})(?=.*\p{Ll})(?=.*\d)(?=.*[\p{P}\p{S}]).{8,}$");
    }

    public override string ToString() => "[Protected]";

    public bool Verify(string password) => BCrypt.Net.BCrypt.Verify(password, Hash);

    public override bool Equals(object? obj)
    {
        if (obj is not Password other) return false;
        return Hash == other.Hash;
    }
    public override int GetHashCode() => Hash.GetHashCode();
}

