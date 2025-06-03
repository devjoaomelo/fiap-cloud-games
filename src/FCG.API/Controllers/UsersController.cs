using System.Security.Claims;
using FCG.Application.UseCases.Users.CreateUser;
using FCG.Application.UseCases.Users.DeleteUser;
using FCG.Application.UseCases.Users.GetAllUsers;
using FCG.Application.UseCases.Users.GetUserByEmail;
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
    private readonly CreateUserHandler _createUserHandler;
    private readonly DeleteUserHandler _deleteUserHandler;
    private readonly GetAllUsersHandler _getAllUsersHandler;
    private readonly GetUserByIdHandler _getUserByIdHandler;
    private readonly GetUserByEmailHandler _getUserByEmailHandler;
    private readonly UpdateUserHandler _updateUserHandler;
    private readonly LoginUserHandler _loginUserHandler;

    public UsersController(
        CreateUserHandler createHandler,
        GetAllUsersHandler getAllHandler,
        GetUserByIdHandler getByIdHandler,
        GetUserByEmailHandler getByEmailHandler,
        UpdateUserHandler updateHandler,
        DeleteUserHandler deleteHandler,
        LoginUserHandler loginUserHandler)
    {
        _createUserHandler = createHandler;
        _deleteUserHandler = deleteHandler;
        _getAllUsersHandler = getAllHandler;
        _getUserByIdHandler = getByIdHandler;
        _getUserByEmailHandler = getByEmailHandler;
        _updateUserHandler = updateHandler;
        _loginUserHandler = loginUserHandler;
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _createUserHandler.HandleCreateUserAsync(request);
        if (result is null)
        {
            throw new Exception("Failed to create user");
        }
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
    
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("Invalid user claim");
        }

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user claim");
        }

        var response = await _getUserByIdHandler.HandleGetUserByIdAsync(new GetUserByIdRequest(userId));

        if (response == null)
        {
            return NotFound("User not found");
        }

        return Ok(response);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllUsersHandler.HandleGetAllUsersAsync(new GetAllUsersRequest());
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getUserByIdHandler.HandleGetUserByIdAsync(new GetUserByIdRequest(id));
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _getUserByEmailHandler.HandleGetUserByEmailAsync(new GetUserByEmailRequest(email));
        
        if (result is null)
        {
            return NotFound("Email not found");
        }
        return Ok(result);
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateSelf([FromBody] UpdateUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (request.Id.ToString() != loggedUserId)
        {
            return Forbid("You can only update your own data.");
        }
        
        var result = await _updateUserHandler.HandleUpdateUserAsync(request);
        return Ok(result);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var response = await _loginUserHandler.HandleLoginUserAsync(request);
        return Ok(response);
    }
}

