using FCG.Application.UseCases.Users.CreateUser;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using Moq;
using Xunit;

namespace FCG.Tests.UseCases;

public class CreateUserHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_CreateUserSuccessfully()
    {
        var mock = new Mock<IUserRepository>();

        mock.Setup(x => x.ExistsUserByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        var handler = new CreateUserHandler(mock.Object);

        var request = new CreateUserRequest("João Melo", "joaomelo@fiap.com", "fiap@1234");

        //act
        var response = await handler.HandleCreateUserAsync(request);

        //assert
        mock.Verify(x=> x.CreateUserAsync(It.IsAny<User>()), Times.Once);

        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Email, response.Email);

    }

    [Fact]
    public async Task Handle_WhenEmailAlreadyExists_ThrowInvalidOperationException()
    {
        var mock = new Mock<IUserRepository>();
        mock.Setup(x => x.ExistsUserByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(true);

        var handler = new CreateUserHandler(mock.Object);
        var request = new CreateUserRequest("João", "joao@fiap.com", "Senha@123");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleCreateUserAsync(request));
        mock.Verify(x => x.CreateUserAsync(It.IsAny<User>()), Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData("emailwithout")]
    [InlineData("joao@")]
    public async Task Handle_WhenEmailIsInvalid_ThrowArgumentException(string invalidEmail)
    {
        // Arrange
        var mock = new Mock<IUserRepository>();
        var handler = new CreateUserHandler(mock.Object);
        var request = new CreateUserRequest("João", invalidEmail, "password@12345");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleCreateUserAsync(request));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Handle_WhenNameIsInvalid_ThrowArgumentException(string invalidName)
    {
        // Arrange
        var mock = new Mock<IUserRepository>();
        var handler = new CreateUserHandler(mock.Object);
        var request = new CreateUserRequest(invalidName, "joao@fiap.com", "password@123");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleCreateUserAsync(request));
    }
}

