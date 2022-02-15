namespace PaymentGateway.Api.Models.Responses
{
    public class PaymentSuccessResponse
    {
        public PaymentSuccessResponse(int paymentId)
        {
            PaymentId = paymentId;
        }
        public int PaymentId { get; private set; }
    }
}
