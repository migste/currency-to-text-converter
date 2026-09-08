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
                long integer = 0;
                int fraction = 0;

                var splittedAmount = amount != null ? amount.Split(',') : System.Array.Empty<string>();

                if (splittedAmount.Length > 0)
                {
                    integer = long.TryParse(splittedAmount[0], out var parsedInteger) ? parsedInteger : 0;

                    if (splittedAmount.Length > 1)
                        fraction = int.TryParse(splittedAmount[1], out var parsedFraction) ? parsedFraction : 0;
                }

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
