using CurrencyToTextConverter.Server.Factories;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyToTextConverter.Server.Controllers
{
    [ApiController]
    [Route("convert")]
    public class ConversionController : ControllerBase
    {

        private readonly CurrencyConverterFactory _currencyConverterFactory;

        private readonly CurrencyDescriptorFactory _currencyDescriptorFactory;


        public ConversionController()
        {
            _currencyConverterFactory = new CurrencyConverterFactory();
            _currencyDescriptorFactory = new CurrencyDescriptorFactory();
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? amount, [FromQuery] string? lang)
        {
            lang ??= "en";

            var converter = _currencyConverterFactory.Create(lang);
            if (converter == null)
                return BadRequest($"Unsupported language: {lang}");


            try
            {
                if (string.IsNullOrWhiteSpace(amount))
                    return BadRequest("Missing amount");

                var normalized = amount.Trim().Replace(',', '.');

                if (!decimal.TryParse(normalized, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out var value))
                    return BadRequest("Invalid amount");

                if (value < 0)
                    return BadRequest("Amount must be non-negative");

                var integer = (long)decimal.Truncate(value);
                // convert fractional part to integer cents (rounded to 2 decimals)
                var fracDecimal = decimal.Round((value - integer) * 100);
                var fraction = (int)fracDecimal; // e.g. 0.01 -> 1, 0.1 -> 10

                var text = converter.Convert(integer, fraction);
                return Ok(new { text });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
