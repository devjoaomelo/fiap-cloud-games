namespace FCG.Application.UseCases.Users.GetUserByEmail;
public class GetUserByEmailRequest
{
    public string Email { get; init; }
    public GetUserByEmailRequest(string email)
    {
        Email = email;
    }
}

