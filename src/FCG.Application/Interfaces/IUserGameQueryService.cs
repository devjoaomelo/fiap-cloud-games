using FCG.Application.UseCases.UserGames.GetGamesByUser;

namespace FCG.Application.Interfaces;

public interface IUserGameQueryService
{
    Task<IEnumerable<GetGamesByUserResponse>> GetGamesByUserAsync(Guid userId);
}