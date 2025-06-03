using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Games.GetGameById;

public class GetGameByIdHandler
{
    private readonly IGameRepository _gameRepository;

    public GetGameByIdHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GetGameByIdResponse> HandleGetGameById(GetGameByIdRequest request)
    {
        var game = await _gameRepository.GetGameByIdAsync(request.GameId);

        if (game == null)
        {
            throw new InvalidOperationException("Game not found.");
        }

        return new GetGameByIdResponse(game.Id, game.Title, game.Description.Text, game.Price, game.CreatedDate );
    }
}