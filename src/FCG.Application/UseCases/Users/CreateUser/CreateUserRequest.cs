using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.Users.CreateUser;
public class CreateUserRequest
{
    [Required]
    public string Name { get; init; }
    [Required]
    public string Email { get; init; }
    [Required]
    public string Password { get; init; }

    public CreateUserRequest(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}

