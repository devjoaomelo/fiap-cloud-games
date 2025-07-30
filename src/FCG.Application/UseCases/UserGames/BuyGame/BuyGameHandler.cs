using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FCG.Application.UseCases.UserGames.BuyGame;

public class BuyGameHandler
{
    private readonly IUserGamePurchaseService _userGamePurchaseService;
    private readonly ILogger<BuyGameHandler> _logger;

    public BuyGameHandler(IUserGamePurchaseService purchaseService, ILogger<BuyGameHandler> logger)
    {
        _userGamePurchaseService = purchaseService;
        _logger = logger;
    }

    public async Task<BuyGameResponse> HandleBuyGameAsync(BuyGameRequest request)
    {
        
        await _userGamePurchaseService.PurchaseGameAsync(request.UserId, request.GameId);
        
        _logger.LogInformation($"Game {request.GameId} added to user {request.UserId}.");
        return new BuyGameResponse(true, "Game purchased successfully.");
    }
}