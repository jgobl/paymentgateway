namespace PaymentGateway.Api.Models.Results.Clients
{
    public class PostPaymentResult
    {
        public PostPaymentResult()
        {
         
        }

        public PostPaymentResult(List<PaymentError> paymentErrors)
        { 
            Errors.AddRange(paymentErrors);
        }
        public List<PaymentError> Errors { get; } = new List<PaymentError>();

        public bool Successful => Errors.Count == 0;
    }
}
