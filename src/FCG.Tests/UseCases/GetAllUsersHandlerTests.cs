using FCG.Application.UseCases.Users.GetAllUsers;
using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Moq;
using Xunit;

namespace FCG.Tests.UseCases;

public class GetAllUsersHandlerTests
{
    [Fact]
    public async Task Handle_WhenUsersExist_ReturnsListOfUsers()
    {
        var mock = new Mock<IUserRepository>();
        var users = new List<User>
        {
            new User("João", new Email("joao@fiap.com"), new Password("Senha@123")),
            new User("Maria", new Email("maria@fiap.com"), new Password("Senha@456"))
        };
        
        mock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);
        
        var handler = new GetAllUsersHandler(mock.Object);
        var request = new GetAllUsersRequest();
        
        var result = (await handler.HandleGetAllUsersAsync(request)).ToList();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        Assert.Equal("João", result[0].Name);
        Assert.Equal("joao@fiap.com", result[0].Email);

        Assert.Equal("Maria", result[1].Name);
        Assert.Equal("maria@fiap.com", result[1].Email);
    }
    [Fact]
    public async Task Handle_WhenNoUsersExist_ShouldThrowInvalidOperationException()
    {

        var mock = new Mock<IUserRepository>();
        mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<User>());

        var handler = new GetAllUsersHandler(mock.Object);
        var request = new GetAllUsersRequest();
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleGetAllUsersAsync(request));
    }
    
}