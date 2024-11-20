using Luhn.DevTest.Core.Utils;
using Xunit;

namespace Luhn.DevTest.Test
{
    public class LuhnValidatorTests
    {
        [Theory]
        [InlineData("4532015112830366", true)] // Valid VISA card
        [InlineData("6011111111111117", true)] // Valid Discover card
        [InlineData("1234567890123456", false)] // Invalid card
        [InlineData("", false)] // Empty input
        [InlineData(null, false)] // Null input
        public void ValidateCreditCard_ShouldReturnCorrectResult(string cardNumber, bool expected)
        {
            bool result = LuhnValidator.ValidateCreditCard(cardNumber);
            Assert.Equal(expected, result);
        }
    }
}
