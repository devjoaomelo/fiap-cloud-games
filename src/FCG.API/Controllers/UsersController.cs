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
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var result = await _createUserHandler.HandleCreateUserAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllUsersHandler.HandleGetAllUsersAsync(new GetAllUsersRequest());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _getUserByIdHandler.HandleGetUserByIdAsync(new GetUserByIdRequest(id));
        return Ok(result);
    }

    [HttpGet("email/{email}")]
    [Authorize]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var result = await _getUserByEmailHandler.HandleGetUserByEmailAsync(new GetUserByEmailRequest(email));
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        if (id != request.Id) return BadRequest("ID mismatch.");
        var result = await _updateUserHandler.HandleUpdateUserAsync(request);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _deleteUserHandler.HandleDeleteUserAsync(new DeleteUserRequest(id));
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

