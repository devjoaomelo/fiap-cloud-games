using System.Security.Claims;
using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FCG.Application.UseCases.Users.CreateUser;
using FCG.Application.UseCases.Users.LoginUser;
using FCG.Application.UseCases.Users.UpdateUser;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserSelfService _selfService;

    public UsersController(IUserSelfService selfService)
    {
        _selfService = selfService;
    }

    // ───────────── POST /api/users (signup) ─────────────
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _selfService.CreateAsync(request);
        return CreatedAtAction(nameof(GetCurrentUser), null, result);
    }

    // ───────────── POST /api/users/login ─────────────
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _selfService.LoginAsync(request);
        return Ok(result);
    }

    // ───────────── GET /api/users/me ─────────────
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
            return Unauthorized("Invalid user claim");

        var result = await _selfService.GetByIdAsync(userId);
        return Ok(result);
    }

    // ───────────── PUT /api/users/me ─────────────
    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserRequest request)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
            return Unauthorized("Invalid user claim");

        request.Id = userId; // substitui o "with" por atribuição direta
        var result = await _selfService.UpdateAsync(request);
        return Ok(result);
    }
}
