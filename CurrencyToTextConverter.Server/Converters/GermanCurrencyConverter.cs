using CurrencyToTextConverter.Server.Descriptors;
using CurrencyToTextConverter.Server.Interfaces;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace CurrencyToTextConverter.Server.Services
{
    internal class GermanCurrencyConverter : ICurrencyConverter
    {
        private static readonly string[] words_0_19 = { "null", "eins", "zwei", "drei", "vier", "fünf", "sechs", "sieben", "acht", "neun", "zehn", "elf", "zwölf", "dreizehn", "vierzehn", "fünfzehn", "sechzehn", "siebzehn", "achtzehn", "neunzehn" };
        private static readonly string[] words_20_99 = { "", "", "zwanzig", "dreißig", "vierzig", "fünfzig", "sechzig", "siebzig", "achtzig", "neunzig" };

        private string _lang = "de";

        public string Convert(long integer, int fraction) { 
            return Convert(integer, fraction, new DollarCurrencyDescriptor());
        }

        public string Convert(long integer, int fraction, ICurrencyDescriptor currencyDescriptor)
        {
            var sb = new StringBuilder();

            sb.Append(integer == 1 ? "ein " + currencyDescriptor.GetIntegerSingular(_lang) : NumberToWords(integer) + " " + currencyDescriptor.GetIntegerPlural(_lang));

            if (fraction > 0)
            {
                sb.Append(" und ");
                if (fraction == 1)
                    // correction for German: "ein" for 1, otherwise "X"
                    sb.Append("ein " + currencyDescriptor.GetFractionSingular(_lang));
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
                // correction for German: "Y", or "einundY" for 1, otherwise "XundY"
                var restWord = "";
                if (rest > 0)
                    restWord = rest == 1 ? restWord = "einund" : words_0_19[rest] + "und"; // e.g. einund, zweiund
                return restWord + words_20_99[ten]; // e.g. vierzig, einundvierzig, zeiundvierzig

            }
            if (number < 1000)
            {
                var hundred = number / 100;
                var rest = number % 100;
                // correction for German: "einhundert" for 100, otherwise "Xhundert"
                var hundredWord = hundred == 1 ? "einhundert" : words_0_19[hundred] + "hundert";
                return hundredWord + (rest > 0 ? NumberToWords(rest) : "");
            }
            if (number < 1000000)
            {
                var thousands = number / 1000;
                var rest = number % 1000;
                // correction for German: "eintausend" for 1000, otherwise "Xtausend"
                var thousandsWord = thousands == 1 ? "eintausend " : NumberToWords(thousands) + "tausend ";
                return thousandsWord + (rest > 0 ? NumberToWords(rest) : "");
            }
            if (number < 1000000000)
            {
                var millions = number / 1000000;
                var rest = number % 1000000;
                // correction for German: "eine Million" for 1, otherwise "X Millionen"
                var millionWord = millions == 1 ? "eine Million " : NumberToWords(millions) + " Millionen ";
                return millionWord + (rest > 0 ? NumberToWords(rest) : "");
            }

            throw new ArgumentOutOfRangeException(nameof(number), "Number must be less than 1.000.000.000");
        }
    }
}
