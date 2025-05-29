using FCG.Infra.Repositories;

namespace FCG.Application.UseCases.Users.GetUserById;
public class GetUserByIdRequest
{
    public Guid Id { get; init; }
    public GetUserByIdRequest(Guid id)
    {
        Id = id;
    }
}

