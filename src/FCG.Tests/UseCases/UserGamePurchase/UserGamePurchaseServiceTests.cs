using Xunit;
using Moq;
using FluentAssertions;

using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;
using FCG.Domain.Interfaces;
using FCG.Domain.Services;

namespace FCG.Tests.UseCases.UserGamePurchase;

public class UserGamePurchaseServiceTests
{
    [Fact]
    public async Task Should_Associate_Game_To_User_When_Valid()
    {
        // Arrange
        var user = new User("João", new Email("joao@email.com"), new Password("Senha@123"));
        var game = new Game(
            new Title("Minecraft"),
            new Description("Jogo de blocos"),
            new Price(99.99m)
        );

        var userRepoMock = new Mock<IUserRepository>();
        var gameRepoMock = new Mock<IGameRepository>();
        var userGameRepoMock = new Mock<IUserGameRepository>();
        var userValidationServiceMock = new Mock<IUserValidationService>();
        var gameValidationServiceMock = new Mock<IGameValidationService>();

        userValidationServiceMock.Setup(s => s.GetUserIfExistsAsync(user.Id)).ReturnsAsync(user);
        gameValidationServiceMock.Setup(s => s.GetGameIfExistsAsync(game.Id)).ReturnsAsync(game);
        userGameRepoMock.Setup(r => r.UserOwnsGameAsync(user.Id, game.Id)).ReturnsAsync(false);

        var service = new UserGamePurchaseService(
            userRepoMock.Object,
            gameRepoMock.Object,
            userGameRepoMock.Object,
            userValidationServiceMock.Object,
            gameValidationServiceMock.Object
        );

        // Act
        Func<Task> act = async () => await service.PurchaseGameAsync(user.Id, game.Id);

        // Assert
        await act.Should().NotThrowAsync();
        userGameRepoMock.Verify(r => r.AddAsync(It.IsAny<UserGame>()), Times.Once);
        userRepoMock.Verify(r => r.UpdateUserAsync(user), Times.Once);
    }
    
    [Fact]
    public async Task Should_Throw_When_User_Already_Owns_Game()
    {
        // Arrange
        var user = new User("João", new Email("joao@email.com"), new Password("Senha@123"));
        var game = new Game(new Title("Minecraft"), new Description("Blocos"), new Price(99.99m));

        var userRepoMock = new Mock<IUserRepository>();
        var gameRepoMock = new Mock<IGameRepository>();
        var userGameRepoMock = new Mock<IUserGameRepository>();
        var userValidationServiceMock = new Mock<IUserValidationService>();
        var gameValidationServiceMock = new Mock<IGameValidationService>();

        userValidationServiceMock.Setup(s => s.GetUserIfExistsAsync(user.Id)).ReturnsAsync(user);
        gameValidationServiceMock.Setup(s => s.GetGameIfExistsAsync(game.Id)).ReturnsAsync(game);
        userGameRepoMock.Setup(r => r.UserOwnsGameAsync(user.Id, game.Id)).ReturnsAsync(true);

        var service = new UserGamePurchaseService(
            userRepoMock.Object,
            gameRepoMock.Object,
            userGameRepoMock.Object,
            userValidationServiceMock.Object,
            gameValidationServiceMock.Object
        );

        // Act
        Func<Task> act = async () => await service.PurchaseGameAsync(user.Id, game.Id);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("User already owns game");
    }
    
    [Fact]
    public async Task Should_Throw_When_Game_Does_Not_Exist()
    {
        // Arrange
        var user = new User("João", new Email("joao@email.com"), new Password("Senha@123"));
        var gameId = Guid.NewGuid();

        var userRepoMock = new Mock<IUserRepository>();
        var gameRepoMock = new Mock<IGameRepository>();
        var userGameRepoMock = new Mock<IUserGameRepository>();
        var userValidationServiceMock = new Mock<IUserValidationService>();
        var gameValidationServiceMock = new Mock<IGameValidationService>();

        userValidationServiceMock.Setup(s => s.GetUserIfExistsAsync(user.Id)).ReturnsAsync(user);
        gameValidationServiceMock.Setup(s => s.GetGameIfExistsAsync(gameId))
            .ThrowsAsync(new ArgumentException("Game not found"));

        var service = new UserGamePurchaseService(
            userRepoMock.Object,
            gameRepoMock.Object,
            userGameRepoMock.Object,
            userValidationServiceMock.Object,
            gameValidationServiceMock.Object
        );

        // Act
        Func<Task> act = async () => await service.PurchaseGameAsync(user.Id, gameId);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }
    
    [Fact]
    public async Task Should_Throw_When_User_Does_Not_Exist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();

        var userRepoMock = new Mock<IUserRepository>();
        var gameRepoMock = new Mock<IGameRepository>();
        var userGameRepoMock = new Mock<IUserGameRepository>();
        var userValidationServiceMock = new Mock<IUserValidationService>();
        var gameValidationServiceMock = new Mock<IGameValidationService>();

        userValidationServiceMock.Setup(s => s.GetUserIfExistsAsync(userId))
            .ThrowsAsync(new ArgumentException("User not found"));

        var service = new UserGamePurchaseService(
            userRepoMock.Object,
            gameRepoMock.Object,
            userGameRepoMock.Object,
            userValidationServiceMock.Object,
            gameValidationServiceMock.Object
        );

        // Act
        Func<Task> act = async () => await service.PurchaseGameAsync(userId, gameId);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }
    
}