using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IUserValidationService
{
    Task<User> GetUserIfExistsAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllUsersAsync();
}