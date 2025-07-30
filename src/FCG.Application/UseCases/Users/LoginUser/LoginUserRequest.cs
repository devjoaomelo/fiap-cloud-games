using System.ComponentModel.DataAnnotations;

namespace FCG.Application.UseCases.Users.LoginUser;

public class LoginUserRequest
{
    [Required, EmailAddress]
    public string Email { get; init; }

    [Required]
    public string Password { get; init; }

    public LoginUserRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
