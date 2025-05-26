using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FCGDbContext _context;

    public UserRepository(FCGDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.Address == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await GetUserByIdAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsUserByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email.Address == email);
    }
}

