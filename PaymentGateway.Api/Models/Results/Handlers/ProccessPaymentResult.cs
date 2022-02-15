namespace PaymentGateway.Api.Models.Results.Handlers
{
    public class ProccessPaymentResult
    {
        public ProccessPaymentResult(int paymentId, List<PaymentError> paymentErrors): this(paymentId)
        {
            Errors.AddRange(paymentErrors);
        }

        public ProccessPaymentResult(int paymentId)
        {
            PaymentId = paymentId;
        }

        public bool Successful => Errors.Count == 0;

        public int PaymentId { get; }
        public List<PaymentError> Errors { get; } = new List<PaymentError>();
    }
}
