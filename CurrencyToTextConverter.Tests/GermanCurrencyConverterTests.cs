using CurrencyToTextConverter.Server.Converters;
using Xunit;

namespace CurrencyToTextConverter.Tests
{
    public class GermanCurrencyConverterTests
    {
        [Theory]
        [InlineData(0, 0, "null Dollar")]
        [InlineData(1, 0, "ein Dollar")]
        [InlineData(2, 0, "zwei Dollar")]
        [InlineData(25, 10, "fünfundzwanzig Dollar und zehn Cent")]
        [InlineData(0, 1, "null Dollar und ein Cent")]
        [InlineData(45100, 0, "fünfundvierzigtausend einhundert Dollar")]
        [InlineData(999999999, 99, "neunhundertneunundneunzig Millionen neunhundertneunundneunzigtausend neunhundertneunundneunzig Dollar und neunundneunzig Cent")]
        public void ConvertToGerman_Expected(long integer, int fraction, string expected)
        {
            var converter = new GermanCurrencyConverter();
            var text = converter.Convert(integer, fraction);
            Assert.Equal(expected, text);
        }
    }
}
