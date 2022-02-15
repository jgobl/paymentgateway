using PaymentGateway.Api.Models.Requests;

namespace PaymentGateway.Integrationtests.Helpers
{
    public class PaymentRequestBuilder
    {
        private readonly PaymentRequest _paymentRequest;

        public PaymentRequestBuilder()
        {
            _paymentRequest = new PaymentRequest
            {
                CardNumber = "155555555555555",
                Cvv = "123",
                ExpiryMonth = "01",
                ExpiryYear = "55",
                MerchantId = 10,
                CurrencyCode = "GBP"
            };
        }

        public PaymentRequestBuilder SetCardNumber(string cardNumber)
        {
            _paymentRequest.CardNumber = cardNumber;
            return this;
        }

        public PaymentRequestBuilder SetCvv(string cvv)
        {
            _paymentRequest.Cvv = cvv;
            return this;
        }

        public PaymentRequestBuilder SetExpiryMonth(string expiryMonth)
        {
            _paymentRequest.ExpiryMonth = expiryMonth;
            return this;
        }

        public PaymentRequestBuilder SetExpiryYear(string expiryYear)
        {
            _paymentRequest.ExpiryYear = expiryYear;
            return this;
        }

        public PaymentRequestBuilder SetMerchantId(int merchantId)
        {
            _paymentRequest.MerchantId = merchantId;
            return this;
        }

        public PaymentRequestBuilder SetCurrencyCode(string currencyCode)
        {
            _paymentRequest.CurrencyCode = currencyCode;
            return this;
        }

        public PaymentRequest Build()
        {
            return _paymentRequest;
        }
    }
}
