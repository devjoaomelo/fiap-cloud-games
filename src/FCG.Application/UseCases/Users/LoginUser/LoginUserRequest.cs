namespace FCG.Application.UseCases.Users.LoginUser;

public class LoginUserRequest
{
    public string Email { get; init; }
    public string Password { get; init; }

    public LoginUserRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
