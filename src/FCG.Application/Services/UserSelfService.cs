using FCG.Application.Interfaces;

namespace FCG.Application.Services;

using FCG.Application.Interfaces;
using FCG.Application.UseCases.Users.CreateUser;
using FCG.Application.UseCases.Users.LoginUser;
using FCG.Application.UseCases.Users.GetUserById;
using FCG.Application.UseCases.Users.UpdateUser;

public class UserSelfService : IUserSelfService
{
    private readonly CreateUserHandler _createHandler;
    private readonly LoginUserHandler _loginHandler;
    private readonly GetUserByIdHandler _getByIdHandler;
    private readonly UpdateUserHandler _updateHandler;

    public UserSelfService(
        CreateUserHandler createHandler,
        LoginUserHandler loginHandler,
        GetUserByIdHandler getByIdHandler,
        UpdateUserHandler updateHandler)
    {
        _createHandler  = createHandler;
        _loginHandler   = loginHandler;
        _getByIdHandler = getByIdHandler;
        _updateHandler  = updateHandler;
    }

    public async Task<CreateUserResponse> CreateAsync(CreateUserRequest request)
        => await _createHandler.HandleCreateUserAsync(request);

    public async Task<LoginUserResponse> LoginAsync(LoginUserRequest request)
        => await _loginHandler.HandleLoginUserAsync(request);

    public async Task<GetUserByIdResponse> GetByIdAsync(Guid id)
        => await _getByIdHandler.HandleGetUserByIdAsync(new GetUserByIdRequest(id));

    public async Task<UpdateUserResponse> UpdateAsync(UpdateUserRequest request)
        => await _updateHandler.HandleUpdateUserAsync(request);
}