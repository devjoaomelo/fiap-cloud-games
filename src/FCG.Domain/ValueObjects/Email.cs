using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects;
public class Email
{
    public string Address { get; private set;}

    public Email(string address)
    {
        if(!IsValid(address))
            throw new ArgumentException("Invalid Email Address");
        Address = address;
    }

    private static bool IsValid(string address)
    {
        return !string.IsNullOrWhiteSpace(address) && Regex.IsMatch(address, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }

    public override string ToString()
    {
        return Address;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Email other) return false;
        return string.Equals(Address, other.Address, StringComparison.OrdinalIgnoreCase);
    }
    public override int GetHashCode() => Address.GetHashCode();
}
