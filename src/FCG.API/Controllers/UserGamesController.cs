using System.Security.Claims;
using FCG.Application.UseCases.UserGames.BuyGame;
using FCG.Application.UseCases.UserGames.GetGamesByUser;
using FCG.Application.UseCases.UserGames.RemoveGameFromUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserGamesController : ControllerBase
{
    private readonly BuyGameHandler _buyGameHandler;
    private readonly GetGamesByUserHandler _getGamesByUserHandler;
    private readonly RemoveGameFromUserHandler _removeGameHandler;

    public UserGamesController(
        BuyGameHandler buyGameHandler,
        GetGamesByUserHandler getGamesByUserHandler,
        RemoveGameFromUserHandler removeGameHandler)
    {
        _buyGameHandler = buyGameHandler;
        _getGamesByUserHandler = getGamesByUserHandler;
        _removeGameHandler = removeGameHandler;
    }

    [Authorize]
    [HttpGet("me/games")]
    public async Task<IActionResult> GetCurrentUserGames()
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

        var response = await _getGamesByUserHandler.HandleGetGamesByUserAsync(new GetGamesByUserRequest(userId));
        if (response == null)
        {
            return NoContent();
        }
        return Ok(response);
    }
    
    [Authorize]
    [HttpPost("games/{gameId:guid}")]
    public async Task<IActionResult> BuyOwnGame(Guid gameId)
    {
        var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(loggedUserId) || !Guid.TryParse(loggedUserId, out var userId))
            return Unauthorized("Invalid user claim");

        var request = new BuyGameRequest(userId, gameId);
        var response = await _buyGameHandler.HandleBuyGameAsync(request);
        return Ok(response);
    }
    
    [Authorize]
    [HttpDelete("games/{gameId:guid}")]
    public async Task<IActionResult> RemoveOwnGame(Guid gameId)
    {
        var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(loggedUserId) || !Guid.TryParse(loggedUserId, out var userId))
            return Unauthorized("Invalid user claim");

        var request = new RemoveGameFromUserRequest(userId, gameId);

        try
        {
            await _removeGameHandler.HandleRemoveGameFromUserAsync(request);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}