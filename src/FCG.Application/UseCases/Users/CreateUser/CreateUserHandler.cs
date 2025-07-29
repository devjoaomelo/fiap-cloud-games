using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.Interfaces;
using FCG.Domain.Services;
using FCG.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases.Users.CreateUser;
public class CreateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly UserCreationService _userCreationService;
    

    public CreateUserHandler(IUserRepository userRepository, ILogger<CreateUserHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
        _userCreationService = new UserCreationService(_userRepository);
    }

    public async Task<CreateUserResponse> HandleCreateUserAsync(CreateUserRequest request)
    {
        var user = new User(request.Name, new Email(request.Email), new Password(request.Password));

        await _userRepository.CreateUserAsync(user);
        _logger.LogInformation($"User {user.Name} added with id {user.Id}");

        return new CreateUserResponse(user.Id, user.Name, user.Email.Address);
    }
}

