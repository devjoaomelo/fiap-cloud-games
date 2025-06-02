using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IUserGameRepository
{
    Task AddAsync(UserGame userGame);
    Task<UserGame?> GetUserGamePurchaseAsync(Guid userId, Guid gameId);
    Task<List<UserGame>> GetGamesByUserIdAsync(Guid userId);
}