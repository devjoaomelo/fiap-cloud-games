namespace FCG.Application.UseCases.Users.CreateUser;
public class CreateUserResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }

    public CreateUserResponse(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}

