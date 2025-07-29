using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Application.UseCases.Games.CreateGame;

public class CreateGameHandler
{
    private readonly IGameRepository _gameRepository;
    private readonly IGameCreationService _gameCreationService;

    public CreateGameHandler(IGameRepository gameRepository, IGameCreationService gameCreationService)
    {
        _gameRepository = gameRepository;
        _gameCreationService = gameCreationService;
    }

    public async Task<CreateGameResponse> HandleCreateGameAsync(CreateGameRequest request)
    {
        var game = await _gameCreationService.CreateGameAsync(
            request.Title,
            request.Description,
            request.Price);

        await _gameRepository.AddGameAsync(game);
        return new CreateGameResponse(game.Id);
    }
}