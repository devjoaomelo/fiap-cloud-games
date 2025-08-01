using FluentAssertions;
using Moq;
using Xunit;

using FCG.Application.UseCases.Users.LoginUser;
using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;
using FCG.Application.Interfaces;
using FCG.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace FCG.Tests.UseCases.LoginUser;

public class LoginUserHandlerTests
{
    [Fact]
    public async Task Should_Return_Token_When_Credentials_Are_Valid()
    {
        // Arrange
        var request = new LoginUserRequest("joao@email.com", "Senha@123");
        var user = new User("João", new Email(request.Email), new Password(request.Password));

        var authServiceMock = new Mock<IUserAuthenticationService>();
        var tokenServiceMock = new Mock<ITokenService>();
        var loggerMock = new Mock<ILogger<LoginUserHandler>>();

        authServiceMock.Setup(s => s.AuthenticateUserAsync(request.Email, request.Password)).ReturnsAsync(user);
        tokenServiceMock.Setup(t => t.GenerateToken(user)).Returns("fake-jwt-token");

        var handler = new LoginUserHandler(authServiceMock.Object, tokenServiceMock.Object, loggerMock.Object);

        // Act
        var result = await handler.HandleLoginUserAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be("fake-jwt-token");
    }

    [Fact]
    public async Task Should_Throw_When_Credentials_Are_Invalid()
    {
        // Arrange
        var request = new LoginUserRequest("joao@email.com", "wrongPassword");

        var authServiceMock = new Mock<IUserAuthenticationService>();
        var tokenServiceMock = new Mock<ITokenService>();
        var loggerMock = new Mock<ILogger<LoginUserHandler>>();

        authServiceMock.Setup(s => s.AuthenticateUserAsync(request.Email, request.Password))
            .ThrowsAsync(new InvalidOperationException("Invalid credentials"));

        var handler = new LoginUserHandler(authServiceMock.Object, tokenServiceMock.Object, loggerMock.Object);

        // Act
        Func<Task> act = async () => await handler.HandleLoginUserAsync(request);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>();
    }
}
