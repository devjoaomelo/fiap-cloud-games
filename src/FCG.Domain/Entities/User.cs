using FCG.Domain.Enums;
using System.Text.RegularExpressions;

namespace FCG.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string HashPassword { get; private set; }
        public Profile Profile { get; private set; }

        protected User() { }

        public User(string name, string email, string hashPassword)
        {
            // Constructor for creating a new user with name, email, and password validation
            SetName(name);
            SetEmail(email);
            SetHashPassword(hashPassword);
            Id = Guid.NewGuid();
            Profile = Profile.User;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name");
            Name = name;
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid Email");
            Email = email;
        }

        private void SetHashPassword(string hashPassword)
        {
            if (string.IsNullOrWhiteSpace(hashPassword))
                throw new ArgumentException("Invalid Password");
            HashPassword = hashPassword;
        }
    }
}
