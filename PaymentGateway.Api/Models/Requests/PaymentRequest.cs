using MediatR;
using PaymentGateway.Api.Models.Results;
using PaymentGateway.Api.Models.Results.Handlers;

namespace PaymentGateway.Api.Models.Requests
{
    public class PaymentRequest : IRequest<ProccessPaymentResult>
    {
        public string? CardNumber { get; set; }
        public string? CurrencyCode { get; set; }
        public string? ExpiryMonth { get; set; }
        public string? ExpiryYear { get; set; }
        public string? Cvv { get; set; }
        public int MerchantId { get; set; }
    }
}
