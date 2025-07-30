using FCG.Application.UseCases.Users.DeleteUser;
using FCG.Application.UseCases.Users.GetAllUsers;
using FCG.Application.UseCases.Users.GetUserByEmail;
using FCG.Application.UseCases.Users.GetUserById;
using FCG.Application.UseCases.Users.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly GetAllUsersHandler    _getAllUsers;
    private readonly GetUserByIdHandler    _getById;
    private readonly GetUserByEmailHandler _getByEmail;
    private readonly UpdateUserHandler     _updateUser;
    private readonly DeleteUserHandler     _deleteUser;

    public AdminController(
        GetAllUsersHandler    getAllUsers,
        GetUserByIdHandler    getById,
        GetUserByEmailHandler getByEmail,
        UpdateUserHandler     updateUser,
        DeleteUserHandler     deleteUser)
    {
        _getAllUsers = getAllUsers;
        _getById     = getById;
        _getByEmail  = getByEmail;
        _updateUser  = updateUser;
        _deleteUser  = deleteUser;
    }

    // ───────────── GET /api/admin/users ─────────────
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _getAllUsers.HandleGetAllUsersAsync(new GetAllUsersRequest()));

    // ───────────── GET /api/admin/users/{id} ─────────────
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await _getById.HandleGetUserByIdAsync(new GetUserByIdRequest(id)));

    // ───────────── GET /api/admin/users/email/{email} ─────────────
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var res = await _getByEmail.HandleGetUserByEmailAsync(new GetUserByEmailRequest(email));
        return res is null ? NotFound("Email not found") : Ok(res);
    }

    // ───────────── PUT /api/admin/users/{id} ─────────────
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest req)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (id != req.Id) return BadRequest("ID mismatch");

        var result = await _updateUser.HandleUpdateUserAsync(req);
        return Ok(result);
    }

    // ───────────── DELETE /api/admin/users/{id} ─────────────
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _deleteUser.HandleDeleteUserAsync(new DeleteUserRequest(id));
        return NoContent();
    }
}
