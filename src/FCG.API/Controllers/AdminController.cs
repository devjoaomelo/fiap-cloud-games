using FCG.Application.UseCases.UserGames.BuyGame;
using FCG.Application.UseCases.UserGames.GetGamesByUser;
using FCG.Application.UseCases.UserGames.RemoveGameFromUser;
using FCG.Application.UseCases.Users.DeleteUser;
using FCG.Application.UseCases.Users.UpdateUser;
using FCG.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly BuyGameHandler _buyGameHandler;
    private readonly GetGamesByUserHandler _getGamesByUserHandler;
    private readonly RemoveGameFromUserHandler _removeGameFromUserHandler;
    private readonly DeleteUserHandler _deleteUserHandler;
    private readonly UpdateUserHandler _updateUserHandler;

    public AdminController(IUserRepository userRepository, BuyGameHandler buyGameHandler, GetGamesByUserHandler getGamesByUserHandler, 
        RemoveGameFromUserHandler removeGameFromUserHandler, DeleteUserHandler deleteUserHandler, UpdateUserHandler updateUserHandler)
    {
        _userRepository = userRepository;
        _buyGameHandler = buyGameHandler;
        _getGamesByUserHandler = getGamesByUserHandler;
        _removeGameFromUserHandler = removeGameFromUserHandler;
        _deleteUserHandler = deleteUserHandler;
        _updateUserHandler = updateUserHandler;
        
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("promote/{id:guid}")]
    public async Task<IActionResult> PromoteUserAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user is null)
        {
            return NotFound("User not found");
        }
        
        user.PromoteToAdmin();
        await _userRepository.UpdateUserAsync(user);
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("{userId:guid}/games/{gameId:guid}")]
    public async Task<IActionResult> BuyGame(Guid userId, Guid gameId)
    {
        var request = new BuyGameRequest(userId, gameId);
        var response = await _buyGameHandler.HandleBuyGameAsync(request);
        return Ok(response);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{userId:guid}/games")]
    public async Task<IActionResult> GetGamesByUserAsync(Guid userId)
    {
        var request = new GetGamesByUserRequest(userId);
        var response = await _getGamesByUserHandler.HandleGetGamesByUserAsync(request);
        return Ok(response);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("users/{id:guid}")]
    public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UpdateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (id != request.Id)
        {
            return BadRequest("Invalid id");
        }

        var result = await _updateUserHandler.HandleUpdateUserAsync(request);
        return Ok(result);
    }

    [HttpDelete("users/{userId:guid}/games/{gameId:guid}")]
    public async Task<IActionResult> RemoveGameFromUserAsync(Guid userId, Guid gameId)
    {
        var request = new RemoveGameFromUserRequest(userId, gameId);

        try
        {
            await _removeGameFromUserHandler.HandleRemoveGameFromUserAsync(request);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var request = new DeleteUserRequest(id);
        try
        {
            await _deleteUserHandler.HandleDeleteUserAsync(request);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        
    }
}