using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.Services;
using FCG.Domain.ValueObjects;

namespace FCG.Application.UseCases.Games.UpdateGame;

public class UpdateGameHandler
{
    private readonly IGameRepository _gameRepository;
    private readonly IGameValidationService _gameValidationService;

    public UpdateGameHandler(IGameRepository gameRepository, IGameValidationService gameValidationService)
    {
        _gameRepository = gameRepository;
        _gameValidationService = gameValidationService;
    }

    public async Task<UpdateGameResponse> HandleUpdateGameAsync(UpdateGameRequest request)
    {
        var game = await _gameValidationService.GetGameIfExistsAsync(request.Id);
        
        game.Update(new Title(request.Title), new Description(request.Description), new Price(request.Price));

        await _gameRepository.UpdateGameAsync(game);

        return new UpdateGameResponse
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description.Text,
            Price = game.Price.Value
        };
    }
}