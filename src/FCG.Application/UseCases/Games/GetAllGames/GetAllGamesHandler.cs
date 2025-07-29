using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Application.UseCases.Games.GetAllGames;

public class GetAllGamesHandler
{
    
    private readonly IGameValidationService _gameValidationService;

    public GetAllGamesHandler(IGameValidationService gameValidationService)
    {
        _gameValidationService = gameValidationService;
    }

    public async Task<IEnumerable<GetAllGamesResponse>> HandleGetAllGamesAsync(GetAllGamesRequest request)
    {
        var games = await _gameValidationService.GetAllGamesAsync();

        var responses = games.Select(game => new GetAllGamesResponse(game.Id,
            game.Title,
            game.Description.Text,
            game.Price,
            game.CreatedDate));
        
        return responses;
    }
}