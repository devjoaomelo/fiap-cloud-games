using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IUserValidationService _userValidationService;

    public UserAuthenticationService(IUserValidationService userValidationService)
    {
        _userValidationService = userValidationService;
    }

    public async Task<User> AuthenticateUserAsync(string email, string password)
    {
        var emailVo = new Email(email);
        
        var user = await _userValidationService.GetUserByEmailAsync(emailVo.Address);
        if(user is null) throw new InvalidOperationException("User or password is incorrect");
        
        if(!BCrypt.Net.BCrypt.Verify(password, user.Password.Hash)) throw new InvalidOperationException("User or password is incorrect");

        return user;

    }
}