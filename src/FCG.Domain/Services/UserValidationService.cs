using FCG.Domain.Entities;
using FCG.Domain.Interfaces;

namespace FCG.Domain.Services;

public class UserValidationService : IUserValidationService
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

    public async Task<User> GetUserByEmailAsync(string email)
    {
        if(email is null) throw new ArgumentException("email can't be empty", nameof(email));
        
        var user = await _userRepository.GetUserByEmailAsync(email);
        
        if(user is null) throw new ArgumentException("User doesn't exists", nameof(email));
        
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        IEnumerable<User> users = await _userRepository.GetAllAsync();
        
        if(users is null || !users.Any()) throw new ArgumentException("No users found", nameof(users));
        
        return users;
    }
}