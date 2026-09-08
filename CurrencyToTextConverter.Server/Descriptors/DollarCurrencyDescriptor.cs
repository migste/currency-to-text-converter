using CurrencyToTextConverter.Server.Interfaces;

namespace CurrencyToTextConverter.Server.Descriptors
{
    public class DollarCurrencyDescriptor : ICurrencyDescriptor
    {

        public string GetIntegerSingular(string lang) { 
            switch (lang.ToLowerInvariant())
            {
                case "de":
                    return "Dollar";
                default:
                    return "dollar";
            }
        }

        public string GetIntegerPlural(string lang)
        {
            switch (lang.ToLowerInvariant())
            {
                case "de":
                    return "Dollar";
                default:
                    return "dollars";
            }
        }

        public string GetFractionSingular(string lang)
        {
            switch (lang.ToLowerInvariant())
            {
                case "de":
                    return "Cent";
                default:
                    return "cent";
            }
        }

        public string GetFractionPlural(string lang)
        {
            switch (lang.ToLowerInvariant())
            {
                case "de":
                    return "Cent";
                default:
                    return "cents";
            }
        }

    }
}
