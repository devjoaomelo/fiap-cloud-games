using FCG.Domain.ValueObjects;
using Xunit;

namespace FCG.Tests.ValueObjects;
public class PasswordTests
{
    [Fact]
    public void Create_ValidPassword_GeneratesHash()
    {
        var password = new Password("StrongP4ss!");
        Assert.NotNull(password.Hash);
        Assert.False(string.IsNullOrWhiteSpace(password.Hash));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("8756")]
    [InlineData("NoSpecialChar234")]
    public void InvalidPasswordEx(string invalidPass)
    {
        Assert.Throws<ArgumentException>(() => new Password(invalidPass));
    }

    [Fact]
    public void Password_MustBe_Equal()
    {
        var password = new Password("Pass1234!");
        Assert.True(password.Verify("Pass1234!"));
    }

    [Fact]
    public void Password_MustBe_Different()
    {
        var password = new Password("Pass1234!");
        Assert.False(password.Verify("WrongPassword123!"));
    }
}

