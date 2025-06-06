﻿using System.ComponentModel.DataAnnotations;
using FCG.Domain.ValueObjects;
using FCG.Domain.Enums;

namespace FCG.Domain.Entities;
public class User
{
    public Guid Id { get; private set; }
    [Required]
    public string Name { get; private set; }
    public Email Email { get; private set; }        
    public Password Password { get; private set; }
    public Profile Profile { get; private set; }
    public ICollection<UserGame> UserGames { get; private set; } = new List<UserGame>();

    protected User()
    {
        Name = null!;
        Email = null!;
        Password = null!;
    }

    public User(string name, Email email, Password password)
    {
        SetName(name);
        Email = email;         
        Password = password;
        Id = Guid.NewGuid();
        Profile = Profile.User;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Invalid Name");
        Name = name;
    }

    public void PromoteToAdmin()
    {
        Profile = Profile.Admin;
    }

    public void Update(string name, Password password)
    {
        SetName(name);
        Password = password;
    }
}

