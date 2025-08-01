using FluentAssertions;
using Moq;
using Xunit;

using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.Interfaces;
using FCG.Domain.Services;
using FCG.Domain.ValueObjects;

namespace FCG.Tests.UseCases.UserCreation;

public class UserCreationServiceTests
{
    [Fact]
    public async Task Should_Create_User_With_User_Profile_When_Not_First()
    {
        // Arrange
        var existingUsers = new List<User>
        {
            new User("Admin", new Email("admin@email.com"), new Password("Admin@123"))
        };

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(r => r.ExistsUserByEmailAsync("joao@email.com")).ReturnsAsync(false);
        userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(existingUsers);

        var service = new UserCreationService(userRepositoryMock.Object);

        // Act
        var user = await service.CreateUserAsync("João", "joao@email.com", "Senha@123");

        // Assert
        user.Name.Should().Be("João");
        user.Email.Address.Should().Be("joao@email.com");
        user.Profile.Should().Be(Profile.User);
    }

    [Fact]
    public async Task Should_Create_User_With_Admin_Profile_When_First_User()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(r => r.ExistsUserByEmailAsync("admin@email.com")).ReturnsAsync(false);
        userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>()); // Nenhum usuário ainda

        var service = new UserCreationService(userRepositoryMock.Object);

        // Act
        var user = await service.CreateUserAsync("Admin", "admin@email.com", "Senha@123");

        // Assert
        user.Profile.Should().Be(Profile.Admin);
    }

    [Fact]
    public async Task Should_Throw_When_Email_Already_Exists()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(r => r.ExistsUserByEmailAsync("joao@email.com")).ReturnsAsync(true);

        var service = new UserCreationService(userRepositoryMock.Object);

        // Act
        Func<Task> act = async () => await service.CreateUserAsync("João", "joao@email.com", "Senha@123");

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Should_Throw_When_Name_Is_Empty()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(r => r.ExistsUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(false);
        userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>());

        var service = new UserCreationService(userRepositoryMock.Object);

        // Act
        Func<Task> act = async () => await service.CreateUserAsync("", "joao@email.com", "Senha@123");

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }
}
