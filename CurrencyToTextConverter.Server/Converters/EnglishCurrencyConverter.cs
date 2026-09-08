using CurrencyToTextConverter.Server.Descriptors;
using CurrencyToTextConverter.Server.Interfaces;
using System.Text;

namespace CurrencyToTextConverter.Server.Services
{
    internal class EnglishCurrencyConverter : ICurrencyConverter
    {
        private static readonly string[] words_0_19 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static readonly string[] words_20_99 = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private string _lang = "en";

        public string Convert(long integer, int fraction) { 
            return Convert(integer, fraction, new DollarCurrencyDescriptor());
        }

        public string Convert(long integer, int fraction, ICurrencyDescriptor currencyDescriptor)
        {
            var sb = new StringBuilder();
            sb.Append(NumberToWords(integer));
            sb.Append(integer == 1 ? " " + currencyDescriptor.GetIntegerSingular(_lang) : " " + currencyDescriptor.GetIntegerPlural(_lang));

            if (fraction > 0)
            {
                sb.Append(" and ");
                if (fraction == 1)
                    sb.Append(NumberToWords(fraction) + " " + currencyDescriptor.GetFractionSingular(_lang));
                else
                    sb.Append(NumberToWords(fraction) + " " + currencyDescriptor.GetFractionPlural(_lang));
            }

            return sb.ToString();
        }

        private string NumberToWords(long number)
        {
            if (number < 0) 
                throw new ArgumentOutOfRangeException(nameof(number), "Number must be a positive integer");
            
            if (number < 20) 
                return words_0_19[number];

            if (number < 100)
            {
                var ten = number / 10;
                var rest = number % 10;
                return words_20_99[ten] + (rest > 0 ? "-" + words_0_19[rest] : "");
            }
            if (number < 1000)
            {
                var hundred = number / 100;
                var rest = number % 100;
                return words_0_19[hundred] + " hundred" + (rest > 0 ? " " + NumberToWords(rest) : "");
            }
            if (number < 1000000)
            {
                var thousands = number / 1000;
                var rest = number % 1000;
                return NumberToWords(thousands) + " thousand" + (rest > 0 ? " " + NumberToWords(rest) : "");
            }
            if (number < 1000000000)
            {
                var millions = number / 1000000;
                var rest = number % 1000000;
                return NumberToWords(millions) + " million" + (rest > 0 ? " " + NumberToWords(rest) : "");
            }

            throw new ArgumentOutOfRangeException(nameof(number), "Number must be less than 1.000.000.000");
        }
    }
}
