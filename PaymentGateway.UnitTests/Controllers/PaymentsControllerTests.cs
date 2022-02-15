using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Api.Models;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Api.Models.Results.Handlers;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.UnitTests.Controllers
{
    public class PaymentsControllerTests
    {
        [Fact]
        public async Task PostPaymentReturnsSuccessfulResponseWhenPaymentSucceeds()
        {
            // arrange
            Mock<IMediator> mediatR = new Mock<IMediator>();
            PaymentsController paymentsController = new PaymentsController(mediatR.Object);
            int createdPaymentId = 500;

            PaymentRequest paymentRequest = new PaymentRequestBuilder().Build();

            mediatR.Setup(stub => stub.Send(paymentRequest, default)).ReturnsAsync(new ProccessPaymentResult(createdPaymentId));

            // act
           var result = await paymentsController.Post(paymentRequest);


            // assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var resultData = Assert.IsAssignableFrom<PaymentSuccessResponse>(createdAtRouteResult.Value);
            Assert.Equal(createdPaymentId, resultData.PaymentId);
        }

        [Fact]
        public async Task PostPaymentReturnsFailedResponseWhenPaymentFails()
        {
            // arrange
            Mock<IMediator> mediatR = new Mock<IMediator>();
            PaymentsController paymentsController = new PaymentsController(mediatR.Object);
            int createdPaymentId = 500;

            PaymentRequest paymentRequest = new PaymentRequestBuilder().Build();

            List<PaymentError> paymentErrors = new List<PaymentError> { new PaymentError("300", "Invalid Card Number") };

            mediatR.Setup(stub => stub.Send(paymentRequest, default)).ReturnsAsync(new ProccessPaymentResult(createdPaymentId, paymentErrors));

            // act
            var result = await paymentsController.Post(paymentRequest);


            // assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.UnprocessableEntity, objectResult.StatusCode);
            var resultData = Assert.IsAssignableFrom<PaymentFailedResponse>(objectResult.Value);
            Assert.Equal(createdPaymentId, resultData.PaymentId);
            Assert.Single(resultData.Errors);
            Assert.Equal(paymentErrors[0].Code, resultData.Errors[0].Code);
        }

        [Fact]
        public async Task GetPaymentReturnsPaymentWhenExists()
        {
            // arrange
            Mock<IMediator> mediatR = new Mock<IMediator>();
            PaymentsController paymentsController = new PaymentsController(mediatR.Object);
            int paymentId = 500;
            PaymentDetailsResponse expectedPaymentDetailsResponse = new PaymentDetailsResponse
            {
                CardNumber = "12345",
                ExpiryMonth = "12",
                ExpiryYear = "23",
                StatusCode = "100",
                CurrencyCode = "GBP"
            };


            mediatR.Setup(stub => stub.Send(It.IsAny<GetPaymentRequest>(), default)).ReturnsAsync(expectedPaymentDetailsResponse);

            // act
            var result = await paymentsController.Get(paymentId);


            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultData = Assert.IsAssignableFrom<PaymentDetailsResponse>(okResult.Value);
            resultData.Should().BeEquivalentTo(expectedPaymentDetailsResponse);
        }

        [Fact]
        public async Task GetPaymentReturnsNotFoundWhenPaymentDoesNotExist()
        {
            // arrange
            Mock<IMediator> mediatR = new Mock<IMediator>();
            PaymentsController paymentsController = new PaymentsController(mediatR.Object);
            int paymentId = 500;
            PaymentDetailsResponse? response = null;

            mediatR.Setup(stub => stub.Send(It.IsAny<GetPaymentRequest>(), default)).ReturnsAsync(response);

            // act
            var result = await paymentsController.Get(paymentId);

            // assert
            Assert.IsType<NotFoundResult>(result);            
        }
    }
}