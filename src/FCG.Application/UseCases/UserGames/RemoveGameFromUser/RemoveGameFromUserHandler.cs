using FCG.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases.UserGames.RemoveGameFromUser;

public class RemoveGameFromUserHandler
{
    private readonly IUserGameRemovalService _removalService;
    private readonly ILogger<RemoveGameFromUserHandler> _logger;

    public RemoveGameFromUserHandler(IUserGameRemovalService removalService,
        ILogger<RemoveGameFromUserHandler> logger)
    {
        _removalService = removalService;
        _logger = logger;
    }

    public async Task<RemoveGameFromUserResponse> HandleRemoveGameAsync(RemoveGameFromUserRequest request)
    {
        await _removalService.RemoveGameAsync(request.UserId, request.GameId);
        _logger.LogInformation("Game {GameId} removed from user {UserId}.",
            request.GameId, request.UserId);

        return new RemoveGameFromUserResponse(true, "Game removed successfully.");
    }
}