using System.Security.Claims;
using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/user-games")]
[Authorize]
public class UserGamesController : ControllerBase
{
    private readonly IUserGameQueryService _queryService;
    private readonly IUserGameCommandService _commandService;

    public UserGamesController(
        IUserGameQueryService queryService,
        IUserGameCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    // ───────────── GET /api/user-games ─────────────
    [HttpGet]
    public async Task<IActionResult> GetMyGames()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
            return Unauthorized("Invalid user claim");

        var response = await _queryService.GetGamesByUserAsync(userId);
        return Ok(response);
    }

    // ───────────── POST /api/user-games/{gameId} ─────────────
    [HttpPost("{gameId:guid}")]
    public async Task<IActionResult> BuyGame(Guid gameId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
            return Unauthorized("Invalid user claim");

        var response = await _commandService.BuyGameAsync(userId, gameId);
        return Ok(response);
    }

    // ───────────── DELETE /api/user-games/{gameId} ─────────────
    [HttpDelete("{gameId:guid}")]
    public async Task<IActionResult> RemoveGame(Guid gameId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
            return Unauthorized("Invalid user claim");

        var response = await _commandService.RemoveGameAsync(userId, gameId);
        return Ok(response);
    }
}