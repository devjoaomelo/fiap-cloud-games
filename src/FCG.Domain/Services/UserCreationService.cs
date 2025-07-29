using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Services;

public class UserCreationService : IUserCreationService
{
    private readonly IUserRepository _userRepository;

    public UserCreationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(string name, string email, string password)
    {
        if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Name cannot be empty", nameof(name));
        
        if(await _userRepository.ExistsUserByEmailAsync(email)) throw new InvalidOperationException($"User with email: {email} already exists");

        var existingUsers = await _userRepository.GetAllAsync();
        var isFirstUser = !existingUsers.Any();
        
        var user = new User(name, new Email(email), new Password(password));

        if (isFirstUser)
        {
            user.PromoteToAdmin();
        }

        return user;
    }
}