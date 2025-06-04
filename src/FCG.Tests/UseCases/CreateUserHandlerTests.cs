using FCG.Application.UseCases.Users.CreateUser;
using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FCG.Tests.UseCases;

public class CreateUserHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<CreateUserHandler>> _mockLogger;
    private readonly CreateUserHandler _handler;

    public CreateUserHandlerTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<CreateUserHandler>>();
        _handler = new CreateUserHandler(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task HandleCreateUserAsync_FirstUser_CreatesAdminUser()
    {
        // Arrange
        var request = new CreateUserRequest("Admin User", "admin@fiap.com", "Admin@123");
        
        _mockRepository.Setup(x => x.ExistsUserByEmailAsync(request.Email))
                      .ReturnsAsync(false);
                      
        _mockRepository.Setup(x => x.GetAllAsync())
                      .ReturnsAsync(new List<User>());

        // Act
        var response = await _handler.HandleCreateUserAsync(request);

        // Assert
        _mockRepository.Verify(x => x.CreateUserAsync(It.Is<User>(u => 
            u.Profile == Profile.Admin)), 
            Times.Once);
            
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Email, response.Email);
        Assert.NotEqual(Guid.Empty, response.Id);
    }

    [Fact]
    public async Task HandleCreateUserAsync_SubsequentUser_CreatesRegularUser()
    {
        // Arrange
        var request = new CreateUserRequest("Regular User", "user@fiap.com", "User@123");
        var existingUser = new User("Admin", new Email("admin@fiap.com"), new Password("Admin@123"));
        
        _mockRepository.Setup(x => x.ExistsUserByEmailAsync(request.Email))
                      .ReturnsAsync(false);
                      
        _mockRepository.Setup(x => x.GetAllAsync())
                      .ReturnsAsync(new List<User> { existingUser });

        // Act
        var response = await _handler.HandleCreateUserAsync(request);

        // Assert
        _mockRepository.Verify(x => x.CreateUserAsync(It.Is<User>(u => 
            u.Profile == Profile.User)), 
            Times.Once);
    }

    [Fact]
    public async Task HandleCreateUserAsync_ExistingEmail_ThrowsException()
    {
        // Arrange
        var request = new CreateUserRequest("Existing User", "existing@fiap.com", "Pass@123");
        
        _mockRepository.Setup(x => x.ExistsUserByEmailAsync(request.Email))
                      .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _handler.HandleCreateUserAsync(request));
            
        _mockLogger.Verify(x => x.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(request.Email)),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task HandleCreateUserAsync_InvalidName_ThrowsArgumentException(string invalidName)
    {
        // Arrange
        var request = new CreateUserRequest(invalidName, "valid@fiap.com", "Valid@123");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _handler.HandleCreateUserAsync(request));
    }

    [Fact]
    public async Task HandleCreateUserAsync_ValidRequest_LogsInformation()
    {
        // Arrange
        var request = new CreateUserRequest("Test User", "test@fiap.com", "Test@123");
        
        _mockRepository.Setup(x => x.ExistsUserByEmailAsync(request.Email))
                      .ReturnsAsync(false);
                      
        _mockRepository.Setup(x => x.GetAllAsync())
                      .ReturnsAsync(new List<User>());

        // Act
        var response = await _handler.HandleCreateUserAsync(request);

        // Assert
        _mockLogger.Verify(x => x.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(response.Id.ToString())),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.AtLeastOnce);
    }
}