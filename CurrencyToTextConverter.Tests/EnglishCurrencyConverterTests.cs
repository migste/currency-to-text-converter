using CurrencyToTextConverter.Server.Converters;
using CurrencyToTextConverter.Server.Factories;
using Xunit;

namespace CurrencyToTextConverter.Tests
{
    public class EnglishCurrencyConverterTests
    {
        [Theory]
        [InlineData(0, 0, "zero dollars")]
        [InlineData(1, 0, "one dollar")]
        [InlineData(2, 0, "two dollars")]
        [InlineData(25, 10, "twenty-five dollars and ten cents")]
        [InlineData(0, 1, "zero dollars and one cent")]
        [InlineData(45100, 0, "forty-five thousand one hundred dollars")]
        [InlineData(999999999, 99, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public void ConvertToEnglish_Expected(long integer, int fraction, string expected)
        {
            var converter = new EnglishCurrencyConverter();
            var descriptor = new CurrencyDescriptorFactory().Create("USD");
            var text = converter.Convert(integer, fraction, descriptor);
            Assert.Equal(expected, text);
        }
    }
}
