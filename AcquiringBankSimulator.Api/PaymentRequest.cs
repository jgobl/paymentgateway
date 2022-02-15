namespace AcquiringBankSimulator.Api
{
    public class PaymentRequest
    {
        public string? CardNumber { get; set; }
        public string? CurrencyCode { get; set; }
        public string? ExpiryMonth { get; set; }
        public string? ExpiryYear { get; set; }
        public string? Cvv { get; set; }
        public int MerchantId { get; set; }
    }
}
