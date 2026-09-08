using CurrencyToTextConverter.Server.Converters;
using CurrencyToTextConverter.Server.Validators;
using Xunit;

namespace CurrencyToTextConverter.Tests
{
    public class GermanCurrencyConverterTests
    {
        [Theory]
        [InlineData("0", "null Dollar")]
        [InlineData("1", "ein Dollar")]
        [InlineData("2", "zwei Dollar")]
        [InlineData("25,1", "fünfundzwanzig Dollar und zehn Cent")]
        [InlineData("0,01", "null Dollar und ein Cent")]
        [InlineData("45 100", "fünfundvierzigtausend einhundert Dollar")]
        [InlineData("999 999 999,99", "neunhundertneunundneunzig Millionen neunhundertneunundneunzigtausend neunhundertneunundneunzig Dollar und neunundneunzig Cent")]
        public void ConvertToGerman_Expected(string amount, string expected)
        {
            var validator = new AmountValidator();
            var result = validator.Validate(amount);
            Assert.True(result.IsValid, result.ErrorMessage);

            var converter = new GermanCurrencyConverter();
            var text = converter.Convert(result.Integer, result.Fraction);
            Assert.Equal(expected, text);
        }
    }
}
