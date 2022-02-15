using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using System.Net;

namespace PaymentGateway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public PaymentsController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentSuccessResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(PaymentFailedResponse))]
        public async Task<IActionResult> Post(PaymentRequest paymentRequest)
        {
            var result = await _mediatR.Send(paymentRequest);

            if (result.Successful)
            {
                var routeValues = new { paymentId = result.PaymentId };
                return CreatedAtRoute("GetPayment", routeValues, new PaymentSuccessResponse(result.PaymentId));
            }

            return new ObjectResult(new PaymentFailedResponse(result.PaymentId, result.Errors)) { StatusCode = (int)HttpStatusCode.UnprocessableEntity };
        }

        [HttpGet("{paymentId}", Name = "GetPayment")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentDetailsResponse))]
        public async Task<IActionResult> Get(int paymentId)
        {
            var result = await _mediatR.Send(new GetPaymentRequest(paymentId));
            if(result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
