using FCG.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases.UserGames.RemoveGameFromUser;

public class RemoveGameFromUserHandler
{
    private readonly IUserGameRepository _userGameRepository;
    private readonly ILogger<RemoveGameFromUserHandler> _logger;

    public RemoveGameFromUserHandler(IUserGameRepository userGameRepository, ILogger<RemoveGameFromUserHandler> logger)
    {
        _userGameRepository = userGameRepository;
        _logger = logger;
    }

    public async Task<RemoveGameFromUserResponse> HandleRemoveGameFromUserAsync(RemoveGameFromUserRequest request)
    {
        var userGame = await _userGameRepository.GetUserGamePurchaseAsync(request.UserId, request.GameId);
        if (userGame == null)
        {
            _logger.LogWarning("User has not purchased this game");
            throw new InvalidOperationException("User has not purchased this game.");
        }
        await _userGameRepository.RemoveUserGameAsync(userGame);
        
        _logger.LogInformation($"Game {request.GameId} removed from user {request.UserId}.");

        return new RemoveGameFromUserResponse(true, "Game has been removed from this user.");
    }
}