using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Application.UseCases.Games.GetGameById;

public class GetGameByIdHandler
{
    private readonly IGameValidationService _gameValidationService;

    public GetGameByIdHandler(IGameValidationService gameValidationService)
    {
        _gameValidationService = gameValidationService;
    }

    public async Task<GetGameByIdResponse> HandleGetGameById(GetGameByIdRequest request)
    {
        var game = await _gameValidationService.GetGameIfExistsAsync(request.GameId);
        
        return new GetGameByIdResponse(game.Id,
            game.Title,
            game.Description.Text,
            game.Price,
            game.CreatedDate );
    }
}