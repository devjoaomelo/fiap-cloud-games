using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IUserCreationService
{
    Task<User> CreateUserAsync(string name, string email, string password);
}