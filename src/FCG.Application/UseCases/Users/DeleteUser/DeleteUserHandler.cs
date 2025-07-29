using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Application.UseCases.Users.DeleteUser;
public class DeleteUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly UserValidationService _userValidationService;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _userValidationService = new UserValidationService(_userRepository);
    }

    public async Task<DeleteUserResponse> HandleDeleteUserAsync(DeleteUserRequest request)
    {
        var user = await _userValidationService.GetUserIfExistsAsync(request.Id);
        
        await _userRepository.DeleteUserAsync(user.Id);

        return new DeleteUserResponse(true, "User removed.");
    }
}

