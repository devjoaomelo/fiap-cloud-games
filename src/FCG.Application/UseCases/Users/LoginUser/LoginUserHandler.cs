using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FCG.Application.UseCases.Users.LoginUser;

public class LoginUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<LoginUserHandler> _logger;

    public LoginUserHandler(IUserRepository userRepository, IConfiguration configuration, ILogger<LoginUserHandler> logger)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<LoginUserResponse> HandleLoginUserAsync(LoginUserRequest loginUserRequest)
    {
        if (string.IsNullOrWhiteSpace(loginUserRequest.Email) || string.IsNullOrWhiteSpace(loginUserRequest.Password))
        {
            throw new ArgumentException("Email or password is required");
        }
        
        var user = await _userRepository.GetUserByEmailAsync(loginUserRequest.Email);
        if (user == null || !user.Password.Verify(loginUserRequest.Password))
        {
            
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = GenerateToken(user);
        _logger.LogInformation($"User {user.Name} logged in");
        return new LoginUserResponse(token);

    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("Jwt:SecretKey not set")));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Address),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Profile.ToString())

        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2), 
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
