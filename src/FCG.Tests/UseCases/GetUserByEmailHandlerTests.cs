using FCG.Application.UseCases.Users.GetUserByEmail;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Moq;
using Xunit;

namespace FCG.Tests.UseCases;

public class GetUserByEmailHandlerTests
{
    [Fact]
    public async Task Handle_ValidEmail_ReturnsUser()
    {
        var mock = new Mock<IUserRepository>();

        var email = "joao@fiap.com";
        var user = new User("João", new Email(email), new Password("Joao@Fiap1234"));

        mock.Setup(x => x.GetUserByEmailAsync(email)).ReturnsAsync(user);
        
        var handler = new GetUserByEmailHandler(mock.Object);
        var request = new GetUserByEmailRequest(email);

        var response = await handler.HandleGetUserByEmailAsync(request);
        Assert.NotNull(response);
        Assert.Equal(user.Id, response.Id);
        Assert.Equal(user.Name, response.Name);
        Assert.Equal(user.Email.Address, response.Email);
    }
    
    [Fact]
    public async Task Handle_InvalidEmail_ThrowsInvalidOperationException()
    {
        var mock = new Mock<IUserRepository>();
        mock.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)null);

        var handler = new GetUserByEmailHandler(mock.Object);
        var request = new GetUserByEmailRequest("notfound@fiap.com");

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleGetUserByEmailAsync(request));
    }
}