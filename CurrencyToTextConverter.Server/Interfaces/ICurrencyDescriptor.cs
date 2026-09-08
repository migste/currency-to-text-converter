namespace CurrencyToTextConverter.Server.Interfaces
{
    internal interface ICurrencyDescriptor
    {
        public string IntegerSingular { get; }
        public string IntegerPlural { get; }
        public string FractionSingular { get; }
        public string FractionPlural { get; }

    }
}
