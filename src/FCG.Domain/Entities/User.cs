using FCG.Domain.ValueObjects;
using FCG.Domain.Enums;

namespace FCG.Domain.Entities;
public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }        
    public Password Password { get; private set; }
    public Profile Profile { get; private set; }
    public ICollection<UserGame> UserGames { get; private set; } = new List<UserGame>();

    protected User() { }

    public User(string name, Email email, Password password)
    {
        SetName(name);
        Email = email ?? throw new ArgumentNullException(nameof(email));         
        Password = password ?? throw new ArgumentNullException(nameof(password));
        Id = Guid.NewGuid();
        Profile = Profile.User;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        Name = name;
    }

    public void PromoteToAdmin()
    {
        Profile = Profile.Admin;
    }

    public void Update(string name, Password password)
    {
        SetName(name);
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
}

