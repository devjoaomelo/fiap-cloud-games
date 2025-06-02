using FCG.Application.UseCases.Games.CreateGame;
using FCG.Application.UseCases.Games.DeleteGame;
using FCG.Application.UseCases.Games.GetAllGames;
using FCG.Application.UseCases.Games.GetGameById;
using FCG.Application.UseCases.Games.UpdateGame;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly CreateGameHandler _createGameHandler;
    private readonly GetAllGamesHandler _getAllGamesHandler;
    private readonly GetGameByIdHandler _getGameByIdHandler;
    private readonly UpdateGameHandler _updateGameHandler;
    private readonly DeleteGameHandler _deleteGameHandler;

    public GamesController(
        CreateGameHandler createGameHandler,
        GetAllGamesHandler getAllGamesHandler,
        GetGameByIdHandler getGameByIdHandler,
        UpdateGameHandler updateGameHandler,
        DeleteGameHandler deleteGameHandler)
    {
        _createGameHandler = createGameHandler;
        _getAllGamesHandler = getAllGamesHandler;
        _getGameByIdHandler = getGameByIdHandler;
        _updateGameHandler = updateGameHandler;
        _deleteGameHandler = deleteGameHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest request)
    {
        var result = await _createGameHandler.HandleCreateGameAsync(request);
        return CreatedAtAction(nameof(GetGameById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGames()
    {
        var result = await _getAllGamesHandler.HandleGetAllGamesAsync(new GetAllGamesRequest());
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetGameById(Guid id)
    {
        var request = new GetGameByIdRequest(id);
        var result = await _getGameByIdHandler.HandleGetGameById(new GetGameByIdRequest(id));
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateGame(Guid id, [FromBody] UpdateGameRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest("Ids do not match");
        }
        var result = await _updateGameHandler.HandleUpdateGameAsync(request);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        await _deleteGameHandler.HandleAsync(new DeleteGameRequest(id));
        return NoContent();
    }
}
