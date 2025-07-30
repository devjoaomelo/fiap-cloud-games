using FCG.Application.UseCases.Users.CreateUser;
using FCG.Application.UseCases.Users.GetUserById;
using FCG.Application.UseCases.Users.LoginUser;
using FCG.Application.UseCases.Users.UpdateUser;

namespace FCG.Application.Interfaces;

public interface IUserSelfService
{
    Task<CreateUserResponse> CreateAsync(CreateUserRequest request);
    Task<LoginUserResponse> LoginAsync(LoginUserRequest request);
    Task<GetUserByIdResponse> GetByIdAsync(Guid id);
    Task<UpdateUserResponse> UpdateAsync(UpdateUserRequest request);
}