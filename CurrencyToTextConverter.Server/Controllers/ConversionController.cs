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
                var integer = long.TryParse(amount?.Split(",")[0], out long parsedInteger) ? parsedInteger : 0;
                var fraction = int.TryParse(amount?.Split(",")[1], out int parsedFraction) ? parsedFraction : 0;

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
