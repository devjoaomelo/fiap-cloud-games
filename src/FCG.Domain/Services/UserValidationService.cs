using FCG.Domain.Entities;
using FCG.Domain.Interfaces;

namespace FCG.Domain.Services;

public class UserValidationService
{
    private readonly IUserRepository _userRepository;

    public UserValidationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserIfExistsAsync(Guid userId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("Invalid user ID", nameof(userId));

        var user = await _userRepository.GetUserByIdAsync(userId);
        
        if(user is null) throw new ArgumentException("User doesn't exists", nameof(userId));

        return user;
    }
}