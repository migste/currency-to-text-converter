using CurrencyToTextConverter.Server.Descriptors;
using CurrencyToTextConverter.Server.Interfaces;
using System.Text;

namespace CurrencyToTextConverter.Server.Services
{
    internal class GermanCurrencyConverter : ICurrencyConverter
    {
        private static readonly string[] words_0_19 = { "null", "eins", "zwei", "drei", "vier", "fünf", "sechs", "sieben", "acht", "neun", "zehn", "elf", "zwölf", "dreizehn", "vierzehn", "fünfzehn", "sechzehn", "siebzehn", "achtzehn", "neunzehn" };
        private static readonly string[] words_20_99 = { "", "", "zwanzig", "dreißig", "vierzig", "fünfzig", "sechzig", "siebzig", "achtzig", "neunzig" };

        public string Convert(long integer, int fraction) { 
            return Convert(integer, fraction, new DollarCurrencyDescriptor());
        }

        public string Convert(long integer, int fraction, ICurrencyDescriptor currencyDescriptor)
        {
            var sb = new StringBuilder();
            sb.Append(NumberToWords(integer));
            sb.Append(integer == 1 ? " " + currencyDescriptor.IntegerSingular : " " + currencyDescriptor.IntegerPlural);

            if (fraction > 0)
            {
                sb.Append(" und ");
                sb.Append(NumberToWords(fraction));
                sb.Append(fraction == 1 ? " " + currencyDescriptor.FractionSingular : " " + currencyDescriptor.FractionPlural);
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
                // Correction for German: "einundY" for x1, otherwise "XundY"
                var unitWord = rest == 1 ? "ein" : words_0_19[rest];
                return unitWord + "und" + words_20_99[ten];

            }
            if (number < 1000)
            {
                var hundred = number / 100;
                var rest = number % 100;
                // Correction for German: "einhundert" for 100, otherwise "Xhundert"
                var hundredWord = hundred == 1 ? "einhundert" : words_0_19[hundred] + "hundert";
                return hundredWord + (rest > 0 ? " " + NumberToWords(rest) : "");
            }
            if (number < 1000000)
            {
                var thousands = number / 1000;
                var rest = number % 1000;
                // Correction for German: "eintausend" for 1000, otherwise "Xtausend"
                var thousandsWord = thousands == 1 ? "eintausend" : NumberToWords(thousands) + "tausend";
                return thousandsWord + (rest > 0 ? " " + NumberToWords(rest) : "");
            }
            if (number < 1000000000)
            {
                var millions = number / 1000000;
                var rest = number % 1000000;
                // Correction for German: "eine Million" for 1, otherwise "X Millionen"
                var millionWord = millions == 1 ? "eine Million" : NumberToWords(millions) + " Millionen";
                return millionWord + (rest > 0 ? " " + NumberToWords(rest) : "");
            }

            throw new ArgumentOutOfRangeException(nameof(number), "Number must be less than 1.000.000.000");
        }
    }
}
