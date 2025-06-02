using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;

namespace FCG.Application.UseCases.Games.CreateGame;

public class CreateGameHandler
{
    private readonly IGameRepository _gameRepository;

    public CreateGameHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<CreateGameResponse> HandleCreateGameAsync(CreateGameRequest request)
    {
        var existingGame = await _gameRepository.GetGameByTitleAsync(request.Title);
        if (existingGame != null)
        {
            throw new ArgumentException($"Game with title '{request.Title}' already exists.");
        }
        var game = new Game(
            new Title(request.Title),
            new Description(request.Description),
            new Price(request.Price)
        );

        await _gameRepository.AddGameAsync(game);
        return new CreateGameResponse(game.Id);

    }
}