using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases.UserGames.BuyGame;

public class BuyGameHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserGameRepository _userGameRepository;
    private readonly ILogger<BuyGameHandler> _logger;

    public BuyGameHandler(
        IUserRepository userRepository,
        IGameRepository gameRepository,
        IUserGameRepository userGameRepository,
        ILogger<BuyGameHandler> logger)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _userGameRepository = userGameRepository;
        _logger = logger;
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
        
        _logger.LogInformation($"Game {request.GameId} added to user {request.UserId}.");

        return new BuyGameResponse(true, "Game purchased successfully.");
    }
}