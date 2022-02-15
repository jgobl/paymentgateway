using PaymentGateway.Api.Models.Data;
using PaymentGateway.Api.Models.Responses;

namespace PaymentGateway.Api.Repositories
{
    public interface IPaymentsRepository
    {
        Task<int> CreatePayment(CreatePaymentData createPaymentData);

        Task<PaymentDetailsResponse> GetPayment(int paymentId);
    }
}
