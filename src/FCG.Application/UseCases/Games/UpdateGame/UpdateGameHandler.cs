using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;

namespace FCG.Application.UseCases.Games.UpdateGame;

public class UpdateGameHandler
{
    private readonly IGameRepository _gameRepository;

    public UpdateGameHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<UpdateGameResponse> HandleAsync(UpdateGameRequest request)
    {
        var game = await _gameRepository.GetGameByIdAsync(request.Id);

        if (game == null)
        {
            throw new InvalidOperationException("Game not found.");
        }


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