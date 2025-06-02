using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IGameRepository
{
    Task AddGameAsync(Game game);
    Task<Game?> GetGameByIdAsync(Guid id);
    Task UpdateGameAsync(Game game);
    Task DeleteGameAsync(Game game);
    Task<IEnumerable<Game>> GetAllGamesAsync();
    Task<Game?> GetGameByTitleAsync(string title);
}