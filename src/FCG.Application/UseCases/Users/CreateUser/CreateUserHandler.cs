using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases.Users.CreateUser;
public class CreateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IUserCreationService _userCreationService;
    

    public CreateUserHandler(IUserRepository userRepository, ILogger<CreateUserHandler> logger, IUserCreationService userCreationService)
    {
        _userRepository = userRepository;
        _logger = logger;
        _userCreationService = userCreationService;
    }

    public async Task<CreateUserResponse> HandleCreateUserAsync(CreateUserRequest request)
    {
        var user = await _userCreationService.CreateUserAsync(request.Name, request.Email, request.Password);

        await _userRepository.CreateUserAsync(user);
        _logger.LogInformation($"User {user.Name} added with id {user.Id}");

        return new CreateUserResponse(user.Id, user.Name, user.Email.Address);
    }
}

