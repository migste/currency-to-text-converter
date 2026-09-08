namespace CurrencyToTextConverter.Server.Interfaces
{
    internal interface ICurrencyDescriptor
    {
        public string GetIntegerSingular(string lang);
        public string GetIntegerPlural(string lang);
        public string GetFractionSingular(string lang);
        public string GetFractionPlural(string lang);
    }
}
