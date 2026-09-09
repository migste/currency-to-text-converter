using CurrencyToTextConverter.Server.Validators;
using Xunit;

namespace CurrencyToTextConverter.Tests
{
    public class AmountParserTests
    {
        [Theory]
        [InlineData("0", 0L, 0)]
        [InlineData("1", 1L, 0)]
        [InlineData("2", 2L, 0)]
        [InlineData("25,1", 25L, 10)]
        [InlineData("0,01", 0L, 1)]
        [InlineData("45 100", 45100L, 0)]
        [InlineData("999 999 999,99", 999999999L, 99)]
        public void TryParse_ValidAmounts_ReturnsExpected(string amount, long expectedInteger, int expectedFraction)
        {
            var parser = new AmountParser();
            var ok = parser.TryParse(amount, out var integer, out var fraction);

            Assert.True(ok);
            Assert.Equal(expectedInteger, integer);
            Assert.Equal(expectedFraction, fraction);
        }

        [Theory]
        [InlineData("1.23")]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData("1,234")]
        public void TryParse_InvalidAmounts_ReturnsFalse(string amount)
        {
            var parser = new AmountParser();
            var ok = parser.TryParse(amount, out var integer, out var fraction);
            Assert.False(ok);
        }
    }
}
