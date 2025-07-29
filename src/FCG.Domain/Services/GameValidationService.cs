using FCG.Domain.Entities;
using FCG.Domain.Interfaces;

namespace FCG.Domain.Services;

public class GameValidationService : IGameValidationService
{
    private readonly IGameRepository _gameRepository;

    public GameValidationService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Game> GetGameIfExistsAsync(Guid gameId)
    {
        if (gameId == Guid.Empty)
            throw new ArgumentException("Invalid game ID.", nameof(gameId));

        var game = await _gameRepository.GetGameByIdAsync(gameId);

        if (game is null)
            throw new InvalidOperationException("Game not found.");

        return game;
    }
}
