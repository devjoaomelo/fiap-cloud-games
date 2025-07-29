using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Application.UseCases.Games.CreateGame;

public class CreateGameHandler
{
    private readonly IGameRepository _gameRepository;
    private readonly GameCreationService _gameCreationService;

    public CreateGameHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
        _gameCreationService = new GameCreationService(_gameRepository);
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