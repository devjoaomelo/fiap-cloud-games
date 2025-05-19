using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects
{
    public class Password
    {
        public string Hash { get; }

        public Password(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash) || !IsValid(hash))
                throw new ArgumentException("Invalid password. Password must be at least 8 characters long, including letters, numbers and special characters.");
            Hash = hash;
        }

        private bool IsValid(string password)
        {
            // Minimum 8 characters, 1 letter, 1 number, 1 special character
            return Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$");
        }

        public override string ToString() => Hash;

        public override bool Equals(object obj)
        {
            if (obj is not Password other) return false;
            return Hash == other.Hash;
        }
        public override int GetHashCode() => Hash.GetHashCode();
    }
}
