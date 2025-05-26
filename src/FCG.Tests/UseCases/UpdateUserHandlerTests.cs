using FCG.Application.UseCases.Users.UpdateUser;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Moq;
using Xunit;

namespace FCG.Tests.UseCases;

public class UpdateUserHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_UpdatesUserSuccessfully()
    {
        var mock = new Mock<IUserRepository>();
        var user = new User("Old Name", new Email("user@fiap.com"), new Password("OldPass@123"));
        mock.Setup(r => r.GetUserByIdAsync(user.Id)).ReturnsAsync(user);

        var handler = new UpdateUserHandler(mock.Object);
        var request = new UpdateUserRequest(user.Id, "New Name", "NewPass@123");

        var response = await handler.HandleUpdateUserAsync(request);

        mock.Verify(r => r.UpdateUserAsync(It.Is<User>(u => u.Id == user.Id)), Times.Once);

        Assert.NotNull(response);
        Assert.Equal(user.Id, response.Id);
        Assert.Equal("New Name", response.Name);
        Assert.Equal(user.Email.Address, response.Email);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsInvalidOperationException()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);

        var handler = new UpdateUserHandler(mockRepo.Object);
        var request = new UpdateUserRequest(Guid.NewGuid(), "Name", "Pass@123");

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleUpdateUserAsync(request));
    }
}