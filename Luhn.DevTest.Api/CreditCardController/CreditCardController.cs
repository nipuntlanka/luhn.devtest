using Microsoft.AspNetCore.Mvc;
using Luhn.DevTest.Core.Utils;

namespace Luhn.DevTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : ControllerBase
    {
        [HttpGet("validate")]
        public IActionResult ValidateCreditCard([FromQuery] string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                return BadRequest("Credit card number is required.");
            }

            bool isValid = LuhnValidator.ValidateCreditCard(cardNumber);
            return Ok(new { CardNumber = cardNumber, IsValid = isValid });
        }
    }
}
