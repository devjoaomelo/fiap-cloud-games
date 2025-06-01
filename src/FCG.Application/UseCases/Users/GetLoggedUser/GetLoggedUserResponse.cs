namespace FCG.Application.UseCases.Users.GetLoggedUser;

public class GetLoggedUserResponse
{
    public Guid Id { get;}
    public string Name { get;}
    public string Email { get;}
    public string Profile { get;}

    public GetLoggedUserResponse(Guid id, string name, string email, string profile)
    {
        Id = id;
        Name = name;
        Email = email;
        Profile = profile;
    }
}