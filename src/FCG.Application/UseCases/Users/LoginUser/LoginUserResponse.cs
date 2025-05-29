namespace FCG.Application.UseCases.Users.LoginUser;

public class LoginUserResponse
{
    public string Token { get; init; }

    public LoginUserResponse(string token)
    {
        Token = token;
    }
}
