using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IUserAuthenticationService
{
    Task<User> AuthenticateUserAsync(string email, string password);
}