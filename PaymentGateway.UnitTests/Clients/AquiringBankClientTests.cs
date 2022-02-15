using FluentAssertions;
using Moq;
using Moq.Protected;
using PaymentGateway.Api.Clients;
using PaymentGateway.Api.Models;
using PaymentGateway.Api.Models.Requests;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.UnitTests.Clients
{
    public class AquiringBankClientTests
    {
        [Fact]
        public async Task PostPaymentReturnsSuccessWhenApiResponseIsSuccessful()
        {
            // arrange
            HttpClient client = BuildMockedHttpClient(HttpStatusCode.Created);
            AcquiringBankClient acquiringBankClient = new AcquiringBankClient(client);
            PaymentRequest paymentRequest = new PaymentRequestBuilder().Build();
            
            // act
            var result = await acquiringBankClient.PostPayment(paymentRequest);

            // assert
            Assert.True(result.Successful);
        }

        [Fact]
        public async Task PostPaymentReturnsUnSuccessfulWithErrorsWhenApiResponseUnProccessable()
        {
            // arrange
            List<PaymentError> paymentErrors = new List<PaymentError> { new PaymentError("400", "cvv error") };
            HttpClient client = BuildMockedHttpClient(HttpStatusCode.UnprocessableEntity, new { Errors = paymentErrors });
            AcquiringBankClient acquiringBankClient = new AcquiringBankClient(client);
            PaymentRequest paymentRequest = new PaymentRequestBuilder().Build();

            // act
            var result = await acquiringBankClient.PostPayment(paymentRequest);

            // assert
            result.Successful.Should().BeFalse();
            result.Errors.Should().BeEquivalentTo(paymentErrors);
        }

        [Fact]
        public async Task PostPaymentReturnsUnSuccessfulWithGenericErrorWhenApiResponseFiveHundred()
        {
            // arrange
            List<PaymentError> paymentErrors = new List<PaymentError> { new PaymentError("500", "A processing error has occurred") };
            HttpClient client = BuildMockedHttpClient(HttpStatusCode.InternalServerError);
            AcquiringBankClient acquiringBankClient = new AcquiringBankClient(client);
            PaymentRequest paymentRequest = new PaymentRequestBuilder().Build();

            // act
            var result = await acquiringBankClient.PostPayment(paymentRequest);

            // assert
            result.Successful.Should().BeFalse();
            result.Errors.Should().BeEquivalentTo(paymentErrors);
        }

        private HttpClient BuildMockedHttpClient(HttpStatusCode httpStatusCode, Object? responseContent = null)
        {
            var responseMessage = new HttpResponseMessage(httpStatusCode);

            if (responseContent != null)
            {
                responseMessage.Content = new StringContent(JsonSerializer.Serialize(responseContent));
            }

            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                  ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                  ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(httpMessageHandler.Object);
            httpClient.BaseAddress = new Uri("http://test.com");            
            return httpClient;
        }
    }
}
