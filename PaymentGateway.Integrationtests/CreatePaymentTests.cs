using PaymentGateway.Api.Extensions;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Integrationtests.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.Integrationtests
{
    public class CreatePaymentTests
    {
        private readonly HttpClient _httpClient;
        private readonly DatabaseHelper _databaseHelper;
        public CreatePaymentTests()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
            _databaseHelper = new DatabaseHelper();

        }

        [Fact]
        public async Task CreatePaymentReturnsSuccessfulAndSavesPaymentWhenPaymentSucceeds()
        {
            // arrange
            PaymentRequest paymentRequest = new PaymentRequestBuilder()
                .SetCardNumber("111111111111111")
                .Build();

            // act
            var result = await _httpClient.PostAsJsonAsync("/api/payments", paymentRequest);

            // assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            var resultData = await result.Content.ReadFromJsonAsync<PaymentSuccessResponse>();
            Assert.NotNull(resultData);
            var paymentData = await _databaseHelper.GetPayment(resultData!.PaymentId);
            Assert.NotNull(paymentData);
            Assert.Equal(paymentRequest!.CardNumber?.Mask(), paymentData.CardNumber);
            Assert.Equal(paymentRequest!.MerchantId, paymentData.MerchantId);
            Assert.Equal("100", paymentData.StatusCode);
            Assert.Equal(paymentRequest!.CurrencyCode, paymentData.CurrencyCode);
            Assert.Equal(paymentRequest!.ExpiryYear, paymentData.ExpiryYear);
            Assert.Equal(paymentRequest!.ExpiryMonth, paymentData.ExpiryMonth);
        }

        [Fact]
        public async Task CreatePaymentReturnsUnSuccessfulAndSavesPaymentWhenPaymentFails()
        {
            // arrange
            PaymentRequest paymentRequest = new PaymentRequestBuilder()
                .SetCardNumber("11111111112222")
                .Build();

            // act
            var result = await _httpClient.PostAsJsonAsync("/api/payments", paymentRequest);

            // assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
            var resultData = await result.Content.ReadFromJsonAsync<PaymentFailedResponse>();
            Assert.NotNull(resultData);
            var paymentData = await _databaseHelper.GetPayment(resultData!.PaymentId);
            Assert.NotNull(paymentData);
            Assert.Equal(paymentRequest!.CardNumber?.Mask(), paymentData.CardNumber);
            Assert.Equal(paymentRequest!.MerchantId, paymentData.MerchantId);
            Assert.Equal("200", paymentData.StatusCode);
            Assert.Equal(paymentRequest!.CurrencyCode, paymentData.CurrencyCode);
            Assert.Equal(paymentRequest!.ExpiryYear, paymentData.ExpiryYear);
            Assert.Equal(paymentRequest!.ExpiryMonth, paymentData.ExpiryMonth);
        }

        // add tests for more scenarios with more time
    }
}