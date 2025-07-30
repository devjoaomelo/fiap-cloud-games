using System.Security.Claims;
using FCG.Application.UseCases.UserGames.BuyGame;
using FCG.Application.UseCases.UserGames.GetGamesByUser;
using FCG.Application.UseCases.UserGames.RemoveGameFromUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/user-games")]
[Authorize]
public class UserGamesController : ControllerBase
{
    private readonly BuyGameHandler               _buyGameHandler;
    private readonly GetGamesByUserHandler        _getGamesByUserHandler;
    private readonly RemoveGameFromUserHandler    _removeGameHandler;

    public UserGamesController(
        BuyGameHandler            buyGameHandler,
        GetGamesByUserHandler     getGamesByUserHandler,
        RemoveGameFromUserHandler removeGameHandler)
    {
        _buyGameHandler        = buyGameHandler;
        _getGamesByUserHandler = getGamesByUserHandler;
        _removeGameHandler     = removeGameHandler;
    }

    // ───────────── GET /api/user-games ─────────────
    [HttpGet]
    public async Task<IActionResult> GetMyGames()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
            return Unauthorized("Invalid user claim");

        var response = await _getGamesByUserHandler.HandleGetGamesByUserAsync(new GetGamesByUserRequest(userId));
        return Ok(response);
    }

    // ───────────── POST /api/user-games/{gameId} ─────────────
    [HttpPost("{gameId:guid}")]
    public async Task<IActionResult> BuyGame(Guid gameId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
            return Unauthorized("Invalid user claim");

        var response = await _buyGameHandler.HandleBuyGameAsync(new BuyGameRequest(userId, gameId));
        return Ok(response);
    }

    // ───────────── DELETE /api/user-games/{gameId} ─────────────
    [HttpDelete("{gameId:guid}")]
    public async Task<IActionResult> RemoveGame(Guid gameId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId))
            return Unauthorized("Invalid user claim");

        await _removeGameHandler.HandleRemoveGameAsync(new RemoveGameFromUserRequest(userId, gameId));
        return NoContent();
    }
}
