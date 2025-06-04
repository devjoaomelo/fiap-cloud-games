using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases.Users.CreateUser;
public class CreateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(IUserRepository userRepository, ILogger<CreateUserHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<CreateUserResponse> HandleCreateUserAsync(CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Name cannot be empty");

        if (await _userRepository.ExistsUserByEmailAsync(request.Email))
        {
            _logger.LogError($"Attempt to create user with existing email: {request.Email}");
            throw new InvalidOperationException("User with this email already exists.");
        }
    
        var existingUsers = await _userRepository.GetAllAsync();
        var isFirstUser = !existingUsers.Any();

        var user = new User(request.Name, new Email(request.Email), new Password(request.Password));

        if (isFirstUser)
        {
            user.PromoteToAdmin();
            _logger.LogInformation($"First user created with admin role: {user.Id}");
        }

        await _userRepository.CreateUserAsync(user);
        _logger.LogInformation($"User {user.Name} added with id {user.Id}");

        return new CreateUserResponse(user.Id, user.Name, user.Email.Address);
    }
}

