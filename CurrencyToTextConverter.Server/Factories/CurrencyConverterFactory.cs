using CurrencyToTextConverter.Server.Interfaces;
using CurrencyToTextConverter.Server.Services;

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
                    _ => null,
                };
            }
     

    }
}
