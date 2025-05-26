using FCG.Application.UseCases.Users.DeleteUser;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Moq;
using Xunit;

namespace FCG.Tests.UseCases;

public class DeleteUserHandlerTests
{
    [Fact]
    public async Task Handle_WhenUserExists_DeletedSuccessfully()
    {
        var mock = new Mock<IUserRepository>();
        
        var userId = Guid.NewGuid();
        var realUser = new User("João", new Email("joao@fiap.com"), new Password("Senha@1234"));
        
        mock.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(realUser);
        
        var handler = new DeleteUserHandler(mock.Object);
        var request = new DeleteUserRequest(userId);
        
        // act
        var response = await handler.HandleDeleteUserAsync(request);
        
        // assert
        mock.Verify(x => x.DeleteUserAsync(realUser.Id), Times.Once);
        Assert.NotNull(response);
        Assert.Equal("User removed.", response.Message);
    }
    
    [Fact]
    public async Task Handle_InvalidId_ThrowsException()
    {
        var mock = new Mock<IUserRepository>();
        
        mock.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);
        
        var handler = new DeleteUserHandler(mock.Object);
        var request = new DeleteUserRequest(Guid.NewGuid());
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleDeleteUserAsync(request));
    }
}