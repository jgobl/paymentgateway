using Moq;
using PaymentGateway.Api.Clients;
using PaymentGateway.Api.Handlers;
using PaymentGateway.Api.Models;
using PaymentGateway.Api.Models.Data;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Results.Clients;
using PaymentGateway.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.UnitTests.Handlers
{
    public class ProcessPaymentHandlerTests
    {
        [Fact]
        public async Task HandleReturnsSuccessfulResponseWhenPaymentSucceeds()
        {
            // arrange
            int createdPaymentId = 10;
            var mockPaymentsRepository = new Mock<IPaymentsRepository>();
            mockPaymentsRepository.Setup(stub => stub.CreatePayment(It.IsAny<CreatePaymentData>()))
                .ReturnsAsync(createdPaymentId);

            var mockAquiringBankClient = new Mock<IAcquiringBankClient>();
            mockAquiringBankClient.Setup(stub => stub.PostPayment(It.IsAny<PaymentRequest>())).ReturnsAsync(new PostPaymentResult());
            
            ProcessPaymentHandler processPaymentHandler = new ProcessPaymentHandler(mockAquiringBankClient.Object, mockPaymentsRepository.Object);

            // act
            var result = await processPaymentHandler.Handle(new PaymentRequestBuilder().Build(), default);

            // assert
            Assert.True(result.Successful);
            Assert.Equal(createdPaymentId, result.PaymentId);
        }

        [Fact]
        public async Task HandleReturnsUnsuccesfulWithErrorsWhenPaymentFails()
        {
            // arrange
            int createdPaymentId = 10;
            var mockPaymentsRepository = new Mock<IPaymentsRepository>();
            mockPaymentsRepository.Setup(stub => stub.CreatePayment(It.IsAny<CreatePaymentData>()))
                .ReturnsAsync(10);

            var paymentErrors = new List<PaymentError> { new PaymentError("500", "Error") };

            var mockAquiringBankClient = new Mock<IAcquiringBankClient>();
            mockAquiringBankClient.Setup(stub => stub.PostPayment(It.IsAny<PaymentRequest>())).ReturnsAsync(new PostPaymentResult(paymentErrors));            

            ProcessPaymentHandler processPaymentHandler = new ProcessPaymentHandler(mockAquiringBankClient.Object, mockPaymentsRepository.Object);

            // act
            var result = await processPaymentHandler.Handle(new PaymentRequestBuilder().Build(), default);

            // assert
            Assert.False(result.Successful);
            Assert.Equal(createdPaymentId, result.PaymentId);
            Assert.Equal(paymentErrors, result.Errors);
        }
    }
}
