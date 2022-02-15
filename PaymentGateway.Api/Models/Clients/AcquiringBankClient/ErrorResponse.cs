namespace PaymentGateway.Api.Models.Clients.AcquiringBankClient
{
    public class ErrorResponse
    {
        public List<PaymentError> Errors { get; set; } = new List<PaymentError>();
    }
}
