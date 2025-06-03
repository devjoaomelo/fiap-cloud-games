using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.UserGames.GetGamesByUser;

public class GetGamesByUserHandler
{
    private readonly IUserGameRepository _userGameRepository;

    public GetGamesByUserHandler(IUserGameRepository userGameRepository)
    {
        _userGameRepository = userGameRepository;
    }

    public async Task<IEnumerable<GetGamesByUserResponse>> HandleGetGamesByUserAsync(GetGamesByUserRequest request)
    {
        var userGames = await _userGameRepository.GetGamesByUserIdAsync(request.UserId);

        return userGames.Select(ug => new GetGamesByUserResponse(
            ug.Game.Id,
            ug.Game.Title,
            ug.Game.Description.Text,
            ug.Game.Price,
            ug.PurchaseDate
        ));
    }
}