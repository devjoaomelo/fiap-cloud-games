using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Application.UseCases.Games.GetGameById;

public class GetGameByIdHandler
{
    private readonly IGameRepository _gameRepository;
    private readonly GameValidationService _gameValidationService;

    public GetGameByIdHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
        _gameValidationService = new GameValidationService(_gameRepository);
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