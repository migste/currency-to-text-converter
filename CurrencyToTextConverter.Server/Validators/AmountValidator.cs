using System;

namespace CurrencyToTextConverter.Server.Validators
{
    public class AmountValidator
    {
        public AmountValidationResult Validate(string? amount)
        {
            if (string.IsNullOrWhiteSpace(amount))
                return new AmountValidationResult { IsValid = false, ErrorMessage = "Missing amount" };

            var trimmedAmount = amount.Trim();

            // separator must be a comma
            if (trimmedAmount.Contains('.'))
                return new AmountValidationResult { IsValid = false, ErrorMessage = "Separator must be ',' (comma)" };

            var parts = trimmedAmount.Split(',');
            if (parts.Length > 2)
                return new AmountValidationResult { IsValid = false, ErrorMessage = "Invalid amount format" };

            var integerPart = parts[0];
            if (string.IsNullOrEmpty(integerPart))
                return new AmountValidationResult { IsValid = false, ErrorMessage = "Invalid integer part" };

            // allow spaces as thousand separators in the integer part (e.g. 40 000)
            var integerDigits = integerPart.Replace(" ", string.Empty);
            if (string.IsNullOrEmpty(integerDigits))
                return new AmountValidationResult { IsValid = false, ErrorMessage = "Invalid integer part" };

            if (!long.TryParse(integerDigits, out var parsedInteger))
                return new AmountValidationResult { IsValid = false, ErrorMessage = "Invalid integer part" };

            if (parsedInteger < 0 || parsedInteger > 999_999_999)
                return new AmountValidationResult { IsValid = false, ErrorMessage = "Integer part must be between 0 and 999,999,999" };

            if (parts.Length == 2)
            {
                var frac = parts[1];
                if (string.IsNullOrEmpty(frac))
                    return new AmountValidationResult { IsValid = false, ErrorMessage = "Invalid fraction part" };

                if (frac.Length > 2)
                    return new AmountValidationResult { IsValid = false, ErrorMessage = "Fraction part must have at most 2 digits" };

                if (frac.Length == 1)
                    frac = frac + "0"; // 1 => 10

                if (!int.TryParse(frac, out var parsedFraction))
                    return new AmountValidationResult { IsValid = false, ErrorMessage = "Invalid fraction part" };

                if (parsedFraction < 0 || parsedFraction > 99)
                    return new AmountValidationResult { IsValid = false, ErrorMessage = "Fraction part must be between 0 and 99" };
            }

            return new AmountValidationResult { IsValid = true };
        }
    }
}
