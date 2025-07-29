using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Application.UseCases.Games.DeleteGame;

public class DeleteGameHandler
{
    private readonly IGameRepository _gameRepository;
    private readonly IGameValidationService _gameValidationService;

    public DeleteGameHandler(IGameRepository gameRepository, IGameValidationService gameValidationService)
    {
        _gameRepository = gameRepository;
        _gameValidationService = gameValidationService;
    }

    public async Task<DeleteGameResponse> HandleDeleteGameAsync(DeleteGameRequest request)
    {
        var game = await _gameValidationService.GetGameIfExistsAsync(request.Id);
        
        await _gameRepository.DeleteGameAsync(game);
        
        return new DeleteGameResponse(true, "Game deleted");
    }
}