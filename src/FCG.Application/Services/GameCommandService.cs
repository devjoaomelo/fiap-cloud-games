namespace FCG.Application.Services;

using FCG.Application.Interfaces;
using FCG.Application.UseCases.Games.CreateGame;
using FCG.Application.UseCases.Games.UpdateGame;
using FCG.Application.UseCases.Games.DeleteGame;

public class GameCommandService : IGameCommandService
{
    private readonly CreateGameHandler _createGame;
    private readonly UpdateGameHandler _updateGame;
    private readonly DeleteGameHandler _deleteGame;

    public GameCommandService(
        CreateGameHandler createGame,
        UpdateGameHandler updateGame,
        DeleteGameHandler deleteGame)
    {
        _createGame = createGame;
        _updateGame = updateGame;
        _deleteGame = deleteGame;
    }

    public async Task<CreateGameResponse> CreateAsync(CreateGameRequest request)
        => await _createGame.HandleCreateGameAsync(request);

    public async Task<UpdateGameResponse> UpdateAsync(UpdateGameRequest request)
        => await _updateGame.HandleUpdateGameAsync(request);

    public async Task<DeleteGameResponse> DeleteAsync(Guid id)
        => await _deleteGame.HandleDeleteGameAsync(new DeleteGameRequest(id));
}