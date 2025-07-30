using FCG.Application.Interfaces;

namespace FCG.Application.Services;

using FCG.Application.Interfaces;
using FCG.Application.UseCases.Users.GetAllUsers;
using FCG.Application.UseCases.Users.GetUserByEmail;
using FCG.Application.UseCases.Users.GetUserById;

public class UserQueryService : IUserQueryService
{
    private readonly GetAllUsersHandler _getAllUsers;
    private readonly GetUserByIdHandler _getUserById;
    private readonly GetUserByEmailHandler _getUserByEmail;

    public UserQueryService(
        GetAllUsersHandler getAllUsers,
        GetUserByIdHandler getUserById,
        GetUserByEmailHandler getUserByEmail)
    {
        _getAllUsers = getAllUsers;
        _getUserById = getUserById;
        _getUserByEmail = getUserByEmail;
    }

    public async Task<IEnumerable<GetAllUsersResponse>> GetAllAsync()
        => await _getAllUsers.HandleGetAllUsersAsync(new GetAllUsersRequest());

    public async Task<GetUserByIdResponse> GetByIdAsync(Guid id)
        => await _getUserById.HandleGetUserByIdAsync(new GetUserByIdRequest(id));

    public async Task<GetUserByEmailResponse> GetByEmailAsync(string email)
        => await _getUserByEmail.HandleGetUserByEmailAsync(new GetUserByEmailRequest(email));
}