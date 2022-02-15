using FluentAssertions;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Validation;
using System.Collections.Generic;
using Xunit;

namespace PaymentGateway.UnitTests.Validation
{
    public class PaymentRequestValidatorTests
    {
        [Fact]
        public void ValidateReturnsIsValidWhenPaymentRequestIsValid()
        {
            // arrange
            PaymentRequest paymentRequest = new PaymentRequestBuilder().Build();

            PaymentRequestValidator paymentRequestValidator = new PaymentRequestValidator();

            // act
            var result = paymentRequestValidator.Validate(paymentRequest);

            // assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ValidateReturnsNotValidWhenPaymentRequestIsNotValid(PaymentRequest paymentRequest)
        {
            // arrange           
            PaymentRequestValidator paymentRequestValidator = new PaymentRequestValidator();

            // act
            var result = paymentRequestValidator.Validate(paymentRequest);

            // assert
            result.IsValid.Should().BeFalse();
        }

        public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { new PaymentRequestBuilder().SetCardNumber("1111").Build() },
            new object[] { new PaymentRequestBuilder().SetCvv("11").Build() },
            new object[] { new PaymentRequestBuilder().SetCurrencyCode("GB").Build() },
            new object[] { new PaymentRequestBuilder().SetExpiryMonth("1111").Build() },
            new object[] { new PaymentRequestBuilder().SetExpiryYear("1111").Build() },
            new object[] { new PaymentRequestBuilder().SetMerchantId(0).Build() }
            // add more test cases if time permitted
        };
    }
}
