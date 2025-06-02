using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Repositories;

public class GameRepository : IGameRepository
{
    private readonly FCGDbContext _context;

    public GameRepository(FCGDbContext context)
    {
        _context = context;
    }

    public async Task AddGameAsync(Game game)
    {
        await _context.AddAsync(game);
        await _context.SaveChangesAsync();
    }

    public async Task<Game?> GetGameByIdAsync(Guid id) =>
        await _context.Games.Include(g => g.UserGames).FirstOrDefaultAsync(g => g.Id == id);
    
    public async Task<IEnumerable<Game>> GetAllGamesAsync() => 
        await _context.Games.ToListAsync();

    public async Task UpdateGameAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGameAsync(Game game)
    {
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }

    public async Task<Game?> GetGameByTitleAsync(string title)
    {
        return await _context.Games.FirstOrDefaultAsync(g => g.Title == title);
    }
}