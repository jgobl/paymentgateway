using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Results.Clients;

namespace PaymentGateway.Api.Clients
{
    public interface IAcquiringBankClient
    {
        Task<PostPaymentResult> PostPayment(PaymentRequest paymentRequest);
    }
}
