using Dapper;
using Npgsql;
using PaymentGateway.Api.Models.Data;
using PaymentGateway.Api.Models.Responses;

namespace PaymentGateway.Api.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly IConfiguration _configuration;

        public PaymentsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> CreatePayment(CreatePaymentData createPaymentData)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("paymentsdb")))
            {
                connection.Open();
                int paymentId = await connection.ExecuteScalarAsync<int>("Insert into public.payment (cardnumber, currencycode, expirymonth, expiryyear, merchantid, statuscode) values (@CardNumber, @CurrencyCode, @ExpiryMonth, @ExpiryYear, @MerchantId, @StatusCode) RETURNING id;", createPaymentData);
                return paymentId;
            }
        }

        public async Task<PaymentDetailsResponse> GetPayment(int paymentId)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("paymentsdb")))
            {
                connection.Open();
                var paymentData = await connection.QuerySingleOrDefaultAsync<PaymentDetailsResponse>("select cardnumber, currencycode, expirymonth, expiryyear, merchantid, statuscode from public.payment where id = @Id;", new { Id = paymentId });
                return paymentData;
            }
        }
    }
}
