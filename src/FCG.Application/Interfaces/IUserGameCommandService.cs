using FCG.Application.UseCases.UserGames.BuyGame;
using FCG.Application.UseCases.UserGames.RemoveGameFromUser;

namespace FCG.Application.Interfaces;

public interface IUserGameCommandService
{
    Task<BuyGameResponse> BuyGameAsync(Guid userId, Guid gameId);
    Task<RemoveGameFromUserResponse> RemoveGameAsync(Guid userId, Guid gameId);
}