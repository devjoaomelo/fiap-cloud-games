using FCG.Domain.Entities;
using FCG.Domain.Interfaces;

namespace FCG.Domain.Services;

public class UserGamePurchaseService : IUserGamePurchaseService
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserGameRepository _userGameRepository;
    private readonly IUserValidationService _userValidationService;
    private readonly IGameValidationService _gameValidationService;

    public UserGamePurchaseService(IUserRepository userRepository, IGameRepository gameRepository, IUserGameRepository userGameRepository,
        IUserValidationService userValidationService, IGameValidationService gameValidationService)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _userValidationService = userValidationService;
        _gameValidationService = gameValidationService;
        _userGameRepository = userGameRepository;
        
    }

    public async Task PurchaseGameAsync(Guid userId, Guid gameId)
    {
        var user = await _userValidationService.GetUserIfExistsAsync(userId);
        var game = await _gameValidationService.GetGameIfExistsAsync(gameId);
        
        if (await _userGameRepository.UserOwnsGameAsync(user.Id, game.Id)) throw new InvalidOperationException("User already owns game");
        
        var userGame = new UserGame(user.Id, game.Id);
        await _userGameRepository.AddAsync(userGame);
        await _userRepository.UpdateUserAsync(user);
        
    }
    
}