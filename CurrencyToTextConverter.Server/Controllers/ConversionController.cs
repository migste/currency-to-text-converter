using Microsoft.AspNetCore.Mvc;

namespace CurrencyToTextConverter.Server.Controllers
{
    [ApiController]
    [Route("convert")]
    public class ConversionController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get([FromQuery] string? amount, [FromQuery] string? lang)
        {
            try
            {
                var text = "test";
                return Ok(new { text });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
