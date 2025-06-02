using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.Games.GetAllGames;

public class GetAllGamesHandler
{
    private readonly IGameRepository _gameRepository;

    public GetAllGamesHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IEnumerable<GetAllGamesResponse>> HandleAsync(GetAllGamesRequest request)
    {
        var games = await _gameRepository.GetAllGamesAsync();

        return games.Select(g => new GetAllGamesResponse(g.Id, g.Title, g.Description.Text, g.Price, g.CreatedDate));
    }
}