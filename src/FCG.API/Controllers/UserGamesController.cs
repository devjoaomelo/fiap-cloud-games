using FCG.Application.UseCases.UserGames.BuyGame;
using FCG.Application.UseCases.UserGames.GetGamesByUser;
using FCG.Application.UseCases.UserGames.RemoveGameFromUser;
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

    [HttpPost("{userId}/games/{gameId}")]
    public async Task<IActionResult> BuyGame(Guid userId, Guid gameId)
    {
        var request = new BuyGameRequest(userId, gameId);
        var response = await _buyGameHandler.HandleBuyGameAsync(request);
        return Ok(response);
    }

    [HttpGet("{userId}/games")]
    public async Task<IActionResult> GetGamesByUser(Guid userId)
    {
        var request = new GetGamesByUserRequest(userId);
        var response = await _getGamesByUserHandler.HandleGetGamesByUserAsync(request);
        return Ok(response);
    }

    [HttpDelete("{userId}/games/{gameId}")]
    public async Task<IActionResult> RemoveGame(Guid userId, Guid gameId)
    {
        var request = new RemoveGameFromUserRequest(userId, gameId);
        await _removeGameHandler.HandleRemoveGameFromUserAsync(request);
        return NoContent();
    }
}
