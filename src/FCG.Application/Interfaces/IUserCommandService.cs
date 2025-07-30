using FCG.Application.UseCases.Users.DeleteUser;
using FCG.Application.UseCases.Users.UpdateUser;

namespace FCG.Application.Interfaces;

public interface IUserCommandService
{
    Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request);
    Task<DeleteUserResponse> DeleteUserAsync(Guid id);
}