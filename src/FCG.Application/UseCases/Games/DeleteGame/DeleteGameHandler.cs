using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Application.UseCases.Games.DeleteGame;

public class DeleteGameHandler
{
    private readonly IGameRepository _gameRepository;
    private readonly GameValidationService _gameValidationService;

    public DeleteGameHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
        _gameValidationService = new GameValidationService(_gameRepository);
    }

    public async Task<DeleteGameResponse> HandleDeleteGameAsync(DeleteGameRequest request)
    {
        var game = await _gameRepository.GetGameByIdAsync(request.Id);
        
        await _gameRepository.DeleteGameAsync(game);
        
        return new DeleteGameResponse(true, "Game deleted");
    }
}