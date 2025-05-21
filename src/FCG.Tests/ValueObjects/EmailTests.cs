using FCG.Domain.ValueObjects;
using Xunit;

namespace FCG.Tests.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void CreateValidEmail()
        {
            var email = new Email("user@gmail.com");
            Assert.Equal("user@gmail.com", email.Address);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("invalid@email")]
        [InlineData("user@.com")]
        public void CreateInvalidEmail(string email)
        {
            Assert.Throws<ArgumentException>(() => new Email(email));
        }

    }
}
