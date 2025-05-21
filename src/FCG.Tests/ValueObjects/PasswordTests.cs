using FCG.Domain.ValueObjects;
using Xunit;

namespace FCG.Tests.ValueObjects
{
    public class PasswordTests
    {
        [Fact]
        public void CreateValidPassword()
        {
            var password = new Password("StrongP4ss!");
            Assert.False(string.IsNullOrWhiteSpace(password.Hash));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("8756")]
        public void InvalidPasswordEx(string invalidPass)
        {
            Assert.Throws<ArgumentException>(() => new Password(invalidPass));
        }

        [Fact]
        public void PasswordMustBeEqual()
        {
            var pass1 = new Password("pass1234!");
            var pass2 = new Password("pass1234!");

            Assert.Equal(pass1, pass2);
        }

        [Fact]
        public void PasswordMustBeDifferent()
        {
            var pass1 = new Password("pass1234!");
            var pass2 = new Password("pass4321!");

            Assert.NotEqual(pass1, pass2);
        }
    }
}
