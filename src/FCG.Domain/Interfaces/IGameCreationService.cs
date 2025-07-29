using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IGameCreationService
{
    Task<Game> CreateGameAsync(string title, string description, decimal price);
}