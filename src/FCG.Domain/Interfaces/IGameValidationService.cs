using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IGameValidationService
{
    Task<Game> GetGameIfExistsAsync(Guid gameId);
}