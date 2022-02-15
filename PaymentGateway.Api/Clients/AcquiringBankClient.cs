using PaymentGateway.Api.Models;
using PaymentGateway.Api.Models.Clients.AcquiringBankClient;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Results.Clients;
using System.Net;

namespace PaymentGateway.Api.Clients
{
    public class AcquiringBankClient : IAcquiringBankClient
    {
        private readonly HttpClient _httpClient;

        public AcquiringBankClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PostPaymentResult> PostPayment(PaymentRequest paymentRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/payments", paymentRequest);

            if(result.IsSuccessStatusCode)
            {
                return new PostPaymentResult();
            }

            if(result.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                var errorResponse = await result.Content.ReadFromJsonAsync<ErrorResponse>();
                return new PostPaymentResult(errorResponse!.Errors);
            }

            return new PostPaymentResult(new List<PaymentError> { new PaymentError("500", "A processing error has occurred") });
        }
    }
}
