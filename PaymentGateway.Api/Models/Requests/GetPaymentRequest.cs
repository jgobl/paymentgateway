using MediatR;
using PaymentGateway.Api.Models.Responses;

namespace PaymentGateway.Api.Models.Requests
{
    public class GetPaymentRequest : IRequest<PaymentDetailsResponse?>
    {
        public GetPaymentRequest(int paymentId)
        {
            PaymentId = paymentId;
        }
        public int PaymentId { get; set;}
    }
}
