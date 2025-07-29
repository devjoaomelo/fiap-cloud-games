using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Application.UseCases.Users.DeleteUser;
public class DeleteUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidationService _userValidationService;

    public DeleteUserHandler(IUserRepository userRepository, IUserValidationService userValidationService)
    {
        _userRepository = userRepository;
        _userValidationService = userValidationService;
    }

    public async Task<DeleteUserResponse> HandleDeleteUserAsync(DeleteUserRequest request)
    {
        var user = await _userValidationService.GetUserIfExistsAsync(request.Id);
        
        await _userRepository.DeleteUserAsync(user.Id);

        return new DeleteUserResponse(true, "User removed.");
    }
}

