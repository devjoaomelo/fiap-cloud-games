using System.Security.Claims;
using FCG.Application.UseCases.Users.CreateUser;
using FCG.Application.UseCases.Users.GetUserById;
using FCG.Application.UseCases.Users.LoginUser;
using FCG.Application.UseCases.Users.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly CreateUserHandler  _createUserHandler;
    private readonly LoginUserHandler   _loginUserHandler;
    private readonly GetUserByIdHandler _getUserByIdHandler;
    private readonly UpdateUserHandler  _updateUserHandler;

    public UsersController(
        CreateUserHandler  createHandler,
        LoginUserHandler   loginHandler,
        GetUserByIdHandler getByIdHandler,
        UpdateUserHandler  updateHandler)
    {
        _createUserHandler  = createHandler;
        _loginUserHandler   = loginHandler;
        _getUserByIdHandler = getByIdHandler;
        _updateUserHandler  = updateHandler;
    }

    // ───────────── POST /api/users  (signup) ─────────────
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _createUserHandler.HandleCreateUserAsync(request);
        return CreatedAtAction(nameof(GetCurrentUser), null, result);
    }

    // ───────────── POST /api/users/login  ─────────────
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var response = await _loginUserHandler.HandleLoginUserAsync(request);
        return Ok(response);
    }

    // ───────────── GET /api/users/me  ─────────────
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
            return Unauthorized("Invalid user claim");

        var response = await _getUserByIdHandler.HandleGetUserByIdAsync(new GetUserByIdRequest(userId));
        return Ok(response);
    }

    // ───────────── PUT /api/users/me  ─────────────
    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateSelf([FromBody] UpdateUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var loggedId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (request.Id.ToString() != loggedId)
            return Forbid("You can only update your own data.");

        var result = await _updateUserHandler.HandleUpdateUserAsync(request);
        return Ok(result);
    }
}
