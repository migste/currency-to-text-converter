namespace CurrencyToTextConverter.Server.Interfaces
{
    public interface ICurrencyConverter
    {
        public string Convert(long integer, int fraction, ICurrencyDescriptor? currencyDescriptor);
    }
}
