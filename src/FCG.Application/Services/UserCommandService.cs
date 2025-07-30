using FCG.Application.Interfaces;

namespace FCG.Application.Services;

using FCG.Application.Interfaces;
using FCG.Application.UseCases.Users.UpdateUser;
using FCG.Application.UseCases.Users.DeleteUser;

public class UserCommandService : IUserCommandService
{
    private readonly UpdateUserHandler _updateUser;
    private readonly DeleteUserHandler _deleteUser;

    public UserCommandService(
        UpdateUserHandler updateUser,
        DeleteUserHandler deleteUser)
    {
        _updateUser = updateUser;
        _deleteUser = deleteUser;
    }

    public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request)
        => await _updateUser.HandleUpdateUserAsync(request);

    public async Task<DeleteUserResponse> DeleteUserAsync(Guid id)
        => await _deleteUser.HandleDeleteUserAsync(new DeleteUserRequest(id));
}