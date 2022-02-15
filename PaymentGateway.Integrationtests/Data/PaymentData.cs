namespace PaymentGateway.Integrationtests.Data
{
    public class PaymentData
    {
        public string? CardNumber { get; set; }
        public string? CurrencyCode { get; set; }
        public string? ExpiryMonth { get; set; }
        public string? ExpiryYear { get; set; }
        public int MerchantId { get; set; }
        public string? StatusCode { get; set; }
    }
}
