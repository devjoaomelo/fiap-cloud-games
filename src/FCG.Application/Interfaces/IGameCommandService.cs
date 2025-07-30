using FCG.Application.UseCases.Games.CreateGame;
using FCG.Application.UseCases.Games.DeleteGame;
using FCG.Application.UseCases.Games.UpdateGame;

namespace FCG.Application.Interfaces;

public interface IGameCommandService
{
    Task<CreateGameResponse> CreateAsync(CreateGameRequest request);
    Task<UpdateGameResponse> UpdateAsync(UpdateGameRequest request);
    Task<DeleteGameResponse> DeleteAsync(Guid id);
}