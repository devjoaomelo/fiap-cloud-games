using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.Users.CreateUser;
public class CreateUserRequest
{
    [Required, MinLength(3)]
    public string Name { get; init; }
    [Required, EmailAddress]
    public string Email { get; init; }
    [Required, MinLength(6)]
    public string Password { get; init; }

    public CreateUserRequest(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}

