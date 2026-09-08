using CurrencyToTextConverter.Server.Interfaces;
using CurrencyToTextConverter.Server.Descriptors;

namespace CurrencyToTextConverter.Server.Factories
{
    internal class CurrencyDescriptorFactory
    {

        public ICurrencyDescriptor? Create(string currency)
        {
            currency = (currency ?? "USD").ToUpperInvariant();

            return currency switch
            {
                "USD" => new DollarCurrencyDescriptor(),
                _ => null,
            };
        }

    }
}
