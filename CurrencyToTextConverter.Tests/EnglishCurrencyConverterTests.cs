using CurrencyToTextConverter.Server.Validators;
using Xunit;
using CurrencyToTextConverter.Server.Converters;

namespace CurrencyToTextConverter.Tests
{
    public class EnglishCurrencyConverterTests
    {
        [Theory]
        [InlineData("0", "zero dollars")]
        [InlineData("1", "one dollar")]
        [InlineData("2", "two dollars")]
        [InlineData("25,1", "twenty-five dollars and ten cents")]
        [InlineData("0,01", "zero dollars and one cent")]
        [InlineData("45 100", "forty-five thousand one hundred dollars")]
        [InlineData("999 999 999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public void ConvertToEnglish_Expected(string amount, string expected)
        {
            var validator = new AmountValidator();
            var result = validator.Validate(amount);
            Assert.True(result.IsValid, result.ErrorMessage);

            var converter = new EnglishCurrencyConverter();
            var text = converter.Convert(result.Integer, result.Fraction);
            Assert.Equal(expected, text);
        }
    }
}
