using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Services;

public class GameCreationService
{
    private readonly IGameRepository _gameRepository;

    public GameCreationService(IGameRepository repository)
    {
        _gameRepository = repository;
    }
    
    public async Task<Game> CreateGameAsync(string title, string description, decimal price)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required");

        var existingGame = await _gameRepository.GetGameByTitleAsync(title);
        if (existingGame != null)
            throw new InvalidOperationException($"A game with title '{title}' already exists.");

        var game = new Game(
            new Title(title),
            new Description(description),
            new Price(price)
        );

        return game;
    }
}