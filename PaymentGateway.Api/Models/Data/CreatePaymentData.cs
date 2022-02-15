namespace PaymentGateway.Api.Models.Data
{
    public class CreatePaymentData
    {
        public CreatePaymentData(string cardNumber, string currencyCode, string expirtyMonth, string expiryYear, int merchantId, string statusCode)
        {
            CardNumber = cardNumber;
            CurrencyCode = currencyCode;
            ExpiryMonth = expirtyMonth;
            ExpiryYear = expiryYear;
            MerchantId = merchantId;
            StatusCode = statusCode;    
        }

        public string CardNumber { get; set; }
        public string CurrencyCode { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public int MerchantId { get; set; }
        public string StatusCode { get; set; }
    }
}
