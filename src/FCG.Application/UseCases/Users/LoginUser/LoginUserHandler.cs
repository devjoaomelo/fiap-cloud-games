using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using FCG.Application.Interfaces;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FCG.Application.UseCases.Users.LoginUser;

public class LoginUserHandler
{
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly ITokenService _tokenService;
    private readonly ILogger<LoginUserHandler> _logger;

    public LoginUserHandler(IUserAuthenticationService userAuthenticationService ,ITokenService tokenService, ILogger<LoginUserHandler> logger)
    {
        _userAuthenticationService = userAuthenticationService;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<LoginUserResponse> HandleLoginUserAsync(LoginUserRequest loginUserRequest)
    {
        var user = await _userAuthenticationService.AuthenticateUserAsync(loginUserRequest.Email, loginUserRequest.Password);
        var token = _tokenService.GenerateToken(user);

        return new LoginUserResponse(token);

    }

}
