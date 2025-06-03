using Xunit;
using Moq;
using FCG.Application.UseCases.UserGames.BuyGame;
using FCG.Domain.Interfaces;
using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;

namespace FCG.Tests.Application.UseCases.UserGames
{
    public class BuyGameHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IUserGameRepository> _userGameRepositoryMock;
        private readonly BuyGameHandler _handler;

        public BuyGameHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _userGameRepositoryMock = new Mock<IUserGameRepository>();

            _handler = new BuyGameHandler(
                _userRepositoryMock.Object,
                _gameRepositoryMock.Object,
                _userGameRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Should_ThrowException_When_UserAlreadyOwnsGame()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();

            var user = new User("John Doe", new Email("john@example.com"), new Password("Password123!"));
            var game = new Game(new Title("Cool Game"), "Fun game", 49.99m);

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            _gameRepositoryMock.Setup(repo => repo.GetGameByIdAsync(gameId))
                .ReturnsAsync(game);
            _userGameRepositoryMock.Setup(repo => repo.GetUserGamePurchaseAsync(userId, gameId))
                .ReturnsAsync(new UserGame(userId, gameId));

            var request = new BuyGameRequest(userId, gameId);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _handler.HandleBuyGameAsync(request));
        }
        
        [Fact]
        public async Task Should_AddGameToUser_When_PurchaseIsValid()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();

            var user = new User("John Doe", new Email("exemple@example.com"),new Password("Password123!"));
            var game = new Game(new Title("Cool Game"), new Description("Fun game"), new Price(49.99m));

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            _gameRepositoryMock.Setup(repo => repo.GetGameByIdAsync(gameId))
                .ReturnsAsync(game);
            _userGameRepositoryMock.Setup(repo => repo.GetUserGamePurchaseAsync(userId, gameId))
                .ReturnsAsync((UserGame?)null); // não possui o jogo

            _userGameRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<UserGame>()))
                .Returns(Task.CompletedTask);

            var request = new BuyGameRequest(userId, gameId);

            // Act
            var response = await _handler.HandleBuyGameAsync(request);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Game purchased successfully.", response.Message);
            _userGameRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<UserGame>()), Times.Once);
        }
        
        [Fact]
        public async Task Should_ThrowException_When_UserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync((User?)null);

            var request = new BuyGameRequest(userId, gameId);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _handler.HandleBuyGameAsync(request));
        }
        
        [Fact]
        public async Task Should_ThrowException_When_GameDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var gameId = Guid.NewGuid();

            var user = new User("John Doe", new Email("john@example.com"), new Password("Password123!"));

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            _gameRepositoryMock.Setup(repo => repo.GetGameByIdAsync(gameId))
                .ReturnsAsync((Game?)null);

            var request = new BuyGameRequest(userId, gameId);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _handler.HandleBuyGameAsync(request));
        }


    }
    
    
}