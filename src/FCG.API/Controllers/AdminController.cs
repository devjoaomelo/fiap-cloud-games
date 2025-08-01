using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FCG.Application.UseCases.Users.UpdateUser;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IUserQueryService _queryService;
    private readonly IUserCommandService _commandService;

    public AdminController(
        IUserQueryService queryService,
        IUserCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    // ───────────── GET /api/admin/users ─────────────
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _queryService.GetAllAsync());

    // ───────────── GET /api/admin/users/{id} ─────────────
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
        => Ok(await _queryService.GetByIdAsync(id));

    // ───────────── GET /api/admin/users/email/{email} ─────────────
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
        => Ok(await _queryService.GetByEmailAsync(email));

    // ───────────── PUT /api/admin/users ─────────────
    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserRequest request)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        return Ok(await _commandService.UpdateUserAsync(request));
    }
        

    // ───────────── DELETE /api/admin/users/{id} ─────────────
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
        => Ok(await _commandService.DeleteUserAsync(id));
}