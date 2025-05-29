using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Users.DeleteUser;
public class DeleteUserHandler
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<DeleteUserResponse> HandleDeleteUserAsync(DeleteUserRequest request)
    {
        var user = await _userRepository.GetUserByIdAsync(request.Id);

        if (user == null)
            throw new InvalidOperationException("User doesn't exist");

        await _userRepository.DeleteUserAsync(user.Id);

        return new DeleteUserResponse(true, "User removed.");
    }
}

