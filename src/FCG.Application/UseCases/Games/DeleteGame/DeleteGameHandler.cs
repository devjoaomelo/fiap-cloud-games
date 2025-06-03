using FCG.Application.UseCases.Users.DeleteUser;
using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Games.DeleteGame;

public class DeleteGameHandler
{
    private readonly IGameRepository _gameRepository;

    public DeleteGameHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<DeleteGameResponse> HandleDeleteGameAsync(DeleteGameRequest request)
    {
        var game = await _gameRepository.GetGameByIdAsync(request.Id);

        if (game is null)
        {
            throw new InvalidOperationException("Game not found.");
        }

        await _gameRepository.DeleteGameAsync(game);
        return new DeleteGameResponse(true, "Game deleted");
    }
}