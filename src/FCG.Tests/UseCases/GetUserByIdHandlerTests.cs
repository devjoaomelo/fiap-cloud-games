using FCG.Application.UseCases.Users.GetUserById;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Moq;
using Xunit;

namespace FCG.Tests.UseCases;

public class GetUserByIdHandlerTests
{
    [Fact]
    public async Task Handle_validId_ReturnsUser()
    {
        var mock = new Mock<IUserRepository>();
        
        var realUser = new User("João Melo", new Email("joao@fiap.com"), new Password("Senha@123"));
        mock.Setup(x => x.GetUserByIdAsync(realUser.Id)).ReturnsAsync(realUser);
        
        var handler = new GetUserByIdHandler(mock.Object);
        var request = new GetUserByIdRequest(realUser.Id);
        
        var response = await handler.HandleGetUserByIdAsync(request);
        
        Assert.NotNull(response);
        Assert.Equal(realUser.Id, response.Id);
        Assert.Equal(realUser.Name, response.Name);
        Assert.Equal(realUser.Email.Address, response.Email);
    }
    [Fact]
    public async Task Handle_UserNotFound_ThrowsInvalidOperationException()
    {
        var mock = new Mock<IUserRepository>();
        mock.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((User?)null);

        var handler = new GetUserByIdHandler(mock.Object);
        var request = new GetUserByIdRequest(Guid.NewGuid());
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleGetUserByIdAsync(request));
    }

}