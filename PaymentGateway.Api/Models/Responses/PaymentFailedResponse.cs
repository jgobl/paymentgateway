namespace PaymentGateway.Api.Models.Responses
{
    public class PaymentFailedResponse
    {
        public PaymentFailedResponse()
        {

        }

        public PaymentFailedResponse(int paymentId, List<PaymentError> paymentErrors)
        {
            PaymentId = paymentId;
            Errors.AddRange(paymentErrors);
        }

        public int PaymentId { get; set; }

        public List<PaymentError> Errors { get; set; } = new List<PaymentError>();
    }
}
