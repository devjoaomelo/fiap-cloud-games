using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Repositories;

public class UserGameRepository : IUserGameRepository
{
    private readonly FCGDbContext _context;

    public UserGameRepository(FCGDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserGame userGame)
    {
        await _context.Set<UserGame>().AddAsync(userGame);
        await _context.SaveChangesAsync();
    }

    public async Task<UserGame?> GetUserGamePurchaseAsync(Guid userId, Guid gameId)
    {
        return await _context.Set<UserGame>()
            .FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GameId == gameId);
    }

    public async Task<List<UserGame>> GetGamesByUserIdAsync(Guid userId)
    {
        return await _context.Set<UserGame>()
            .Include(ug => ug.Game)
            .Where(ug => ug.UserId == userId)
            .ToListAsync();
    }

    public async Task RemoveUserGameAsync(UserGame userGame)
    {
        _context.Set<UserGame>().Remove(userGame);
        await _context.SaveChangesAsync();
    }
}