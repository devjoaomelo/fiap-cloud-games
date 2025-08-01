using FluentAssertions;
using Moq;
using Xunit;

using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.Services;
using FCG.Domain.ValueObjects;

namespace FCG.Tests.UseCases.GameCreation;

public class GameCreationServiceTests
{
    [Fact]
    public async Task Should_Create_Game_When_Title_Is_Unique()
    {
        // Arrange
        var gameRepositoryMock = new Mock<IGameRepository>();
        gameRepositoryMock.Setup(r => r.GetGameByTitleAsync("Minecraft")).ReturnsAsync((Game?)null);

        var service = new GameCreationService(gameRepositoryMock.Object);

        // Act
        var game = await service.CreateGameAsync("Minecraft", "Blocos", 99.99m);

        // Assert
        game.Title.Name.Should().Be("Minecraft");
        game.Description.Text.Should().Be("Blocos");
        game.Price.Value.Should().Be(99.99m);
    }

    [Fact]
    public async Task Should_Throw_When_Title_Is_Empty()
    {
        // Arrange
        var gameRepositoryMock = new Mock<IGameRepository>();
        var service = new GameCreationService(gameRepositoryMock.Object);

        // Act
        Func<Task> act = async () => await service.CreateGameAsync("", "desc", 10.0m);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task Should_Throw_When_Game_Already_Exists()
    {
        // Arrange
        var existingGame = new Game(
            new Title("Minecraft"),
            new Description("desc"),
            new Price(99.99m)
        );

        var gameRepositoryMock = new Mock<IGameRepository>();
        gameRepositoryMock.Setup(r => r.GetGameByTitleAsync("Minecraft")).ReturnsAsync(existingGame);

        var service = new GameCreationService(gameRepositoryMock.Object);

        // Act
        Func<Task> act = async () => await service.CreateGameAsync("Minecraft", "desc", 99.99m);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>();
    }
}
