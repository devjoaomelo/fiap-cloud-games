using FCG.Application.UseCases.Games.GetAllGames;
using FCG.Application.UseCases.Games.GetGameById;

namespace FCG.Application.Interfaces;

public interface IGameQueryService
{
    Task<IEnumerable<GetAllGamesResponse>> GetAllAsync();
    Task<GetGameByIdResponse> GetByIdAsync(Guid id);
}