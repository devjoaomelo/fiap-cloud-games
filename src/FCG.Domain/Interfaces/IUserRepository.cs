using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;
public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
    Task<bool> ExistsUserByEmailAsync (string email);
}

