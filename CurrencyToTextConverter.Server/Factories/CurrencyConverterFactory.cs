using CurrencyToTextConverter.Server.Interfaces;
using CurrencyToTextConverter.Server.Converters;

namespace CurrencyToTextConverter.Server.Factories
{
    internal class CurrencyConverterFactory
    {

            public ICurrencyConverter? Create(string lang)
            {
                lang = (lang ?? "en").ToLowerInvariant();
                return lang switch
                {
                    "en" => new EnglishCurrencyConverter(),
                    "de" => new GermanCurrencyConverter(),
                    _ => null,
                };
            }
     

    }
}
