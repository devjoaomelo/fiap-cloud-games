namespace FCG.Domain.Interfaces;

public interface IUserGameRemovalService
{
    Task RemoveGameAsync(Guid userId, Guid gameId);
}