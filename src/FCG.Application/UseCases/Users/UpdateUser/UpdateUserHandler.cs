using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases.Users.UpdateUser;
public class UpdateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UpdateUserHandler> _logger;

    public UpdateUserHandler(IUserRepository userRepository, ILogger<UpdateUserHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UpdateUserResponse> HandleUpdateUserAsync(UpdateUserRequest request)
    {
        var user = await _userRepository.GetUserByIdAsync(request.Id);

        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }
        
        var newPassword = new Password(request.NewPassword);
        user.Update(request.NewName, newPassword);

        await _userRepository.UpdateUserAsync(user);
        _logger.LogInformation($"User {user.Name} updated");
        return new UpdateUserResponse(user.Id, user.Name, user.Email.Address);
    }
}

