using FCG.Domain.Interfaces;

namespace FCG.Domain.Services;

public class UserGameRemovalService : IUserGameRemovalService
{
    private readonly IUserGameRepository _userGameRepository;
    private readonly IUserValidationService _userValidationService;
    private readonly IGameValidationService _gameValidationService;

    public UserGameRemovalService(
        IUserGameRepository userGameRepository,
        IUserValidationService userValidationService,
        IGameValidationService gameValidationService)
    {
        _userGameRepository = userGameRepository;
        _userValidationService = userValidationService;
        _gameValidationService = gameValidationService;
    }

    public async Task RemoveGameAsync(Guid userId, Guid gameId)
    {
        await _userValidationService.GetUserIfExistsAsync(userId);
        await _gameValidationService.GetGameIfExistsAsync(gameId);

        var userGame = await _userGameRepository.GetUserGamePurchaseAsync(userId, gameId);
        if (userGame is null)
            throw new InvalidOperationException("Usuário não possui este jogo.");

        await _userGameRepository.RemoveUserGameAsync(userGame);
        
    }
}