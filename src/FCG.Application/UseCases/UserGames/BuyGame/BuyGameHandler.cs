using FCG.Domain.Entities;
using FCG.Domain.Interfaces;

namespace FCG.Application.UseCases.UserGames.BuyGame;

public class BuyGameHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserGameRepository _userGameRepository;

    public BuyGameHandler(
        IUserRepository userRepository,
        IGameRepository gameRepository,
        IUserGameRepository userGameRepository)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _userGameRepository = userGameRepository;
    }

    public async Task<BuyGameResponse> HandleBuyGameAsync(BuyGameRequest request)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId)
                   ?? throw new InvalidOperationException("User not found.");

        var game = await _gameRepository.GetGameByIdAsync(request.GameId)
                   ?? throw new InvalidOperationException("Game not found.");

        var existingPurchase = await _userGameRepository.GetUserGamePurchaseAsync(request.UserId, request.GameId);
        if (existingPurchase != null)
        {
            throw new InvalidOperationException("Game already purchased by user.");
        }

        var userGame = new UserGame(request.UserId, request.GameId);
        await _userGameRepository.AddAsync(userGame);

        return new BuyGameResponse(userGame.Id);
    }
}