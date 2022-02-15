namespace PaymentGateway.Api.Models
{
    public class PaymentError
    {
        public PaymentError(string code, string description)
        {
            Code = code;
            Description = description;
        }
        public string Code { get; set; }

        public string Description { get; set; }
    }
}
