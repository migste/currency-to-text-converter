using CurrencyToTextConverter.Server.Interfaces;

namespace CurrencyToTextConverter.Server.Descriptors
{
    internal class DollarCurrencyDescriptor : ICurrencyDescriptor
    {
        public string IntegerSingular => "dollar";
        public string IntegerPlural => "dollars";
        public string FractionSingular => "cent";
        public string FractionPlural => "cents";

       
    }
}
