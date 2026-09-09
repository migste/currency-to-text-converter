using System;

namespace CurrencyToTextConverter.Server.Validators
{
    public class AmountParser
    {
        public bool TryParse(string? amount, out long integer, out int fraction)
        {
            integer = 0;
            fraction = 0;

            if (string.IsNullOrEmpty(amount))
            {
                return false;
            }

            var parts = amount.Split(',');
            if (parts.Length > 2)
            {
                return false;
            }

            // remove spaces from integer part and try parse
            var intPart = parts[0].Trim().Replace(" ", "");
            if (!long.TryParse(intPart, out integer))
                return false;

            if (parts.Length == 1)
            {
                fraction = 0;
                return true;
            }

            var fracRaw = parts[1].Trim();
            if (fracRaw.Length == 0 || fracRaw.Length > 2)
                return false;

            if (!int.TryParse(fracRaw, out fraction))
                return false;

            if (fracRaw.Length == 1)
                fraction *= 10;

            return true;
        }
    }
}
