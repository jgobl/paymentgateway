using MediatR;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Api.Repositories;

namespace PaymentGateway.Api.Handlers
{
    public class GetPaymentHandler : IRequestHandler<GetPaymentRequest, PaymentDetailsResponse?>
    {
        private readonly IPaymentsRepository _paymentsRepository;

        public GetPaymentHandler(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public async Task<PaymentDetailsResponse?> Handle(GetPaymentRequest request, CancellationToken cancellationToken)
        {
            var paymentDetails = await _paymentsRepository.GetPayment(request.PaymentId);
            return paymentDetails;
        }
    }
}
