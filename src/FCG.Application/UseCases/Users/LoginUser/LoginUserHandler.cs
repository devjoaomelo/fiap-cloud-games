using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FCG.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FCG.Application.UseCases.Users.LoginUser;

public class LoginUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public LoginUserHandler(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<LoginUserResponse> HandleLoginUserAsync(LoginUserRequest loginUserRequest)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginUserRequest.Email);

        if (user == null || user.Password.Value != loginUserRequest.Password)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.Address),
                new Claim(ClaimTypes.Role, user.Profile.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new LoginUserResponse(tokenHandler.WriteToken(token));

    }
}
