using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AcquiringBankSimulator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(PaymentRequest paymentRequest)
        {            
            if (paymentRequest!.CardNumber!.Equals("111111111111111"))
            {
                return Ok();
            }

            if (paymentRequest!.CardNumber!.Equals("500111111111111"))
            {
                return StatusCode(500);
            }

            return new ObjectResult(new { Errors = new[] { new { Code = "100", Description = "Cvv failure" } } }) { StatusCode = (int)HttpStatusCode.UnprocessableEntity };
        }
    }
}
