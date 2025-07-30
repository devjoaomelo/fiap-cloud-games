using FCG.Application.Interfaces;

namespace FCG.Application.Services;

using FCG.Application.Interfaces;
using FCG.Application.UseCases.UserGames.BuyGame;
using FCG.Application.UseCases.UserGames.RemoveGameFromUser;

public class UserGameCommandService : IUserGameCommandService
{
    private readonly BuyGameHandler _buyGameHandler;
    private readonly RemoveGameFromUserHandler _removeGameHandler;

    public UserGameCommandService(
        BuyGameHandler buyGameHandler,
        RemoveGameFromUserHandler removeGameHandler)
    {
        _buyGameHandler = buyGameHandler;
        _removeGameHandler = removeGameHandler;
    }

    public async Task<BuyGameResponse> BuyGameAsync(Guid userId, Guid gameId)
    {
        var request = new BuyGameRequest(userId, gameId);
        return await _buyGameHandler.HandleBuyGameAsync(request);
    }

    public async Task<RemoveGameFromUserResponse> RemoveGameAsync(Guid userId, Guid gameId)
    {
        var request = new RemoveGameFromUserRequest(userId, gameId);
        return await _removeGameHandler.HandleRemoveGameAsync(request);
    }
}