using CurrencyToTextConverter.Server.Validators;
using Xunit;

namespace CurrencyToTextConverter.Tests
{
    public class AmountValidatorTests
    {
        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("25,1")]
        [InlineData("0,01")]
        [InlineData("45 100")]
        [InlineData("999 999 999,99")]
        public void Validate_ValidAmounts_ReturnsValid(string amount)
        {
            var validator = new AmountValidator();
            var result = validator.Validate(amount);
            Assert.True(result.IsValid, result.ErrorMessage);
        }

        [Theory]
        [InlineData("-1", "Integer part must be between 0 and 999,999,999")]
        [InlineData("1.23", "Separator must be ',' (comma)")]
        [InlineData("1000000000", "Integer part must be between 0 and 999,999,999")]
        [InlineData("", "Missing amount")]
        public void Validate_InvalidAmounts_ReturnsInvalid(string amount, string expectedError)
        {
            var validator = new AmountValidator();
            var result = validator.Validate(amount);
            Assert.False(result.IsValid);
            Assert.Equal(expectedError, result.ErrorMessage);
        }
    }
}
