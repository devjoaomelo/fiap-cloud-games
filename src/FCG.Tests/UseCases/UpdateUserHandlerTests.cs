using FCG.Application.UseCases.Users.UpdateUser;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FCG.Tests.UseCases;

public class UpdateUserHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<UpdateUserHandler>> _mockLogger;
    private readonly UpdateUserHandler _handler;

    public UpdateUserHandlerTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<UpdateUserHandler>>();
        _handler = new UpdateUserHandler(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_UpdatesUserSuccessfully()
    {
        // Arrange
        var user = new User("Old Name", new Email("user@fiap.com"), new Password("OldPass@123"));
        _mockRepository.Setup(r => r.GetUserByIdAsync(user.Id)).ReturnsAsync(user);
        var request = new UpdateUserRequest(user.Id, "New Name", "NewPass@123");

        // Act
        var response = await _handler.HandleUpdateUserAsync(request);

        // Assert
        _mockRepository.Verify(r => r.UpdateUserAsync(It.Is<User>(u => u.Id == user.Id)), Times.Once);
        Assert.NotNull(response);
        Assert.Equal(user.Id, response.Id);
        Assert.Equal("New Name", response.Name);
        Assert.Equal(user.Email.Address, response.Email);
        
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("updated")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((User?)null);
        var request = new UpdateUserRequest(userId, "Name", "Pass@123");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.HandleUpdateUserAsync(request));
        
        _mockRepository.Verify(r => r.UpdateUserAsync(It.IsAny<User>()), Times.Never);
    }
}