using FCG.Application.Interfaces;
using FCG.Application.UseCases.UserGames.GetGamesByUser;
using FCG.Domain.Entities;

namespace FCG.Application.Services;

public class UserGameQueryService : IUserGameQueryService
{
    private readonly GetGamesByUserHandler _getGamesByUserHandler;

    public UserGameQueryService(GetGamesByUserHandler getGamesByUserHandler)
    {
        _getGamesByUserHandler = getGamesByUserHandler;
    }

    public async Task<IEnumerable<GetGamesByUserResponse>> GetGamesByUserAsync(Guid userId)
    {
        var request = new GetGamesByUserRequest(userId);
        return await _getGamesByUserHandler.HandleGetGamesByUserAsync(request);
    }
}