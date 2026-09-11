using CurrencyToTextConverter.Server.Factories;
using CurrencyToTextConverter.Server.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyToTextConverter.Server.Controllers
{
    [ApiController]
    [Route("convert")]
    public class ConversionController : ControllerBase
    {
        private readonly CurrencyConverterFactory _currencyConverterFactory;
        private readonly CurrencyDescriptorFactory _currencyDescriptorFactory;

        public ConversionController(CurrencyConverterFactory currencyConverterFactory, CurrencyDescriptorFactory currencyDescriptorFactory)
        {
            _currencyConverterFactory = currencyConverterFactory;
            _currencyDescriptorFactory = currencyDescriptorFactory;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? amount, [FromQuery] string? lang, [FromQuery] string? currency)
        {
            lang ??= "en";
            currency ??= "USD";

            var converter = _currencyConverterFactory.Create(lang);
            if (converter == null)
                return NotFound($"Unsupported language: {lang}");

            var descriptor = _currencyDescriptorFactory.Create(currency);
            if (descriptor == null)
                return NotFound($"Unsupported currency: {currency}");


            try
            {
                var validator = new AmountValidator();
                var result = validator.Validate(amount);
                if (!result.IsValid)
                    return BadRequest(result.ErrorMessage ?? "Invalid amount");

                var parser = new AmountParser();
                if (!parser.TryParse(amount, out var integer, out var fraction))
                    return StatusCode(500, "Internal Server Error: Unable to parse amount");

                var text = converter.Convert(integer, fraction, descriptor);
                return Ok(new { text });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
