namespace FCG.Application.Services;

using FCG.Application.Interfaces;
using FCG.Application.UseCases.Games.GetAllGames;
using FCG.Application.UseCases.Games.GetGameById;

public class GameQueryService : IGameQueryService
{
    private readonly GetAllGamesHandler _getAllGames;
    private readonly GetGameByIdHandler _getGameById;

    public GameQueryService(
        GetAllGamesHandler getAllGames,
        GetGameByIdHandler getGameById)
    {
        _getAllGames = getAllGames;
        _getGameById = getGameById;
    }

    public async Task<IEnumerable<GetAllGamesResponse>> GetAllAsync()
        => await _getAllGames.HandleGetAllGamesAsync(new GetAllGamesRequest());

    public async Task<GetGameByIdResponse> GetByIdAsync(Guid id)
        => await _getGameById.HandleGetGameById(new GetGameByIdRequest(id));
}