namespace PaymentGateway.Api.Models.Responses
{
    public class PaymentDetailsResponse
    {
        public string? CardNumber { get; set; }
        public string? CurrencyCode { get; set; }
        public string? ExpiryMonth { get; set; }
        public string? ExpiryYear { get; set; }

        public string? StatusCode { get; set; }
    }
}
