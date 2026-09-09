using CurrencyToTextConverter.Server.Validators;
using Xunit;

namespace CurrencyToTextConverter.Tests
{
    public class AmountValidatorTests
    {
        [Theory]
        [InlineData("0", 0L, 0)]
        [InlineData("1", 1L, 0)]
        [InlineData("2", 2L, 0)]
        [InlineData("25,1", 25L, 10)]
        [InlineData("0,01", 0L, 1)]
        [InlineData("45 100", 45100L, 0)]
        [InlineData("999 999 999,99", 999999999L, 99)]
        public void Validate_ValidAmounts_ReturnsExpected(string amount, long expectedInteger, int expectedFraction)
        {
            var validator = new AmountValidator();
            var result = validator.Validate(amount);

            Assert.True(result.IsValid, result.ErrorMessage);
            Assert.Equal(expectedInteger, result.Integer);
            Assert.Equal(expectedFraction, result.Fraction);
        }

        [Theory]
        [InlineData("-1", "Integer part must be between 0 and 999,999,999")]
        [InlineData("1.23", "Separator must be ',' (comma)")]
        [InlineData("1000000000", "Integer part must be between 0 and 999,999,999")]
        public void Validate_InvalidAmounts_ReturnsInvalid(string amount, string expectedError)
        {
            var validator = new AmountValidator();
            var result = validator.Validate(amount);

            Assert.False(result.IsValid);
            Assert.Equal(expectedError, result.ErrorMessage);
        }
    }
}
