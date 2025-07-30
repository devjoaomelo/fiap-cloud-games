using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FCG.Application.UseCases.Games.CreateGame;
using FCG.Application.UseCases.Games.UpdateGame;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameQueryService _queryService;
    private readonly IGameCommandService _commandService;

    public GamesController(
        IGameQueryService queryService,
        IGameCommandService commandService)
    {
        _queryService = queryService;
        _commandService = commandService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _commandService.CreateAsync(request);
        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllGames()
    {
        var response = await _queryService.GetAllAsync();
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetGameById(Guid id)
    {
        var response = await _queryService.GetByIdAsync(id);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateGame([FromBody] UpdateGameRequest request)
    {
        var response = await _commandService.UpdateAsync(request);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        var response = await _commandService.DeleteAsync(id);
        return Ok(response);
    }
}