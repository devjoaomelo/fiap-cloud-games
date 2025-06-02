using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Games.DeleteGame;

public class DeleteGameHandler
{
    private readonly IGameRepository _gameRepository;

    public DeleteGameHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task HandleAsync(DeleteGameRequest request)
    {
        var game = await _gameRepository.GetGameByIdAsync(request.Id);

        if (game == null)
        {
            throw new InvalidOperationException("Game not found.");
        }

        await _gameRepository.DeleteGameAsync(game);
    }
}