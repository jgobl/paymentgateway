using MediatR;
using PaymentGateway.Api.Clients;
using PaymentGateway.Api.Extensions;
using PaymentGateway.Api.Models.Data;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Results.Handlers;
using PaymentGateway.Api.Repositories;

namespace PaymentGateway.Api.Handlers
{
    public class ProcessPaymentHandler : IRequestHandler<PaymentRequest, ProccessPaymentResult>
    {
        private readonly IAcquiringBankClient _aquiringBankClient;
        private readonly IPaymentsRepository _paymentsRespository;

        public ProcessPaymentHandler(IAcquiringBankClient aquiringBankClient, IPaymentsRepository paymentsRespository)
        {
            _aquiringBankClient = aquiringBankClient;
            _paymentsRespository = paymentsRespository;
        }

        public async Task<ProccessPaymentResult> Handle(PaymentRequest request, CancellationToken cancellationToken)
        {

            var result = await _aquiringBankClient.PostPayment(request);
            
            var paymentId = await _paymentsRespository.CreatePayment(new CreatePaymentData(
                request.CardNumber!.Mask(),
                request.CurrencyCode!,
                request.ExpiryMonth!,
                request.ExpiryYear!,
                request.MerchantId,
                result.Successful ? "100" : "200"));

            if(result.Successful)
            {
                return new ProccessPaymentResult(paymentId);
            }

            return new ProccessPaymentResult(paymentId, result.Errors);
            
        }
    }
}
