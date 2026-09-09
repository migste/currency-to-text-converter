using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyToTextConverter.Server.Validators
{
    public sealed class AmountValidationResult
    {
        public bool IsValid { get; init; }
        public string? ErrorMessage { get; init; }
    }
}
