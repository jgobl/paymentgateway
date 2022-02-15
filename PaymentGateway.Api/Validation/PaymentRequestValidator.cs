using FluentValidation;
using PaymentGateway.Api.Models.Requests;

namespace PaymentGateway.Api.Validation
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            RuleFor(paymentRequest => paymentRequest.MerchantId).GreaterThan(0);
            RuleFor(paymentRequest => paymentRequest.CardNumber)
                .NotEmpty()
                .Matches("^[0-9]{13,15}$");
            RuleFor(paymentRequest => paymentRequest.CurrencyCode)
                .NotEmpty()
                .Matches("^[a-zA-Z]{3}$"); // would have further validation to match against what CurrencyCodes are supported

            RuleFor(paymentRequest => paymentRequest.Cvv)
                .NotEmpty()
                .Matches("^[0-9]{3,4}$");

            RuleFor(paymentRequest => paymentRequest.ExpiryYear)
                .NotEmpty()
                .Matches("^[0-9]{2}$");

            RuleFor(paymentRequest => paymentRequest.ExpiryMonth)
                .NotEmpty()
                .Matches("^[0-9]{2}$");

            // Note expiry year and month could be validated further to make sure
            // the combination is not before the current year and month. I decided
            // not to tackle that in the time I have as by only accepting two digits for both there are edge cases such
            // as if the year is close to ending in 00 then would we allow 03
            // and validate against the year 3003 instead of 2003? etc.
        }
    }
}
