using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IUserGamePurchaseService
{
    Task PurchaseGameAsync(Guid userId, Guid gameId);
}