namespace FCG.Application.UseCases.Users.UpdateUser;

public class UpdateUserResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }

    public UpdateUserResponse(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}

