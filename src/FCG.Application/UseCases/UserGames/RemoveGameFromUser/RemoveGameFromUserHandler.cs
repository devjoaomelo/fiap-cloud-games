using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.UserGames.RemoveGameFromUser;

public class RemoveGameFromUserHandler
{
    private readonly IUserGameRepository _userGameRepository;

    public RemoveGameFromUserHandler(IUserGameRepository userGameRepository)
    {
        _userGameRepository = userGameRepository;
    }

    public async Task<RemoveGameFromUserResponse> HandleRemoveGameFromUserAsync(RemoveGameFromUserRequest request)
    {
        var userGame = await _userGameRepository.GetUserGamePurchaseAsync(request.UserId, request.GameId);
        if (userGame == null)
        {
            throw new InvalidOperationException("User has not purchased this game.");
        }

        await _userGameRepository.RemoveUserGameAsync(userGame);

        return new RemoveGameFromUserResponse(true, "Game has been removed from this user.");
    }
}