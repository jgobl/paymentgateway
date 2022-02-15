using Dapper;
using Npgsql;
using PaymentGateway.Integrationtests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Integrationtests.Helpers
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;
        public DatabaseHelper()
        {
            _connectionString = "Server=localhost;Port=5432;Database=paymentgatewaydb;User Id=postgres;Password=postgres;";
        }

        public async Task<PaymentData> GetPayment(int paymentId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var paymentData = await connection.QuerySingleAsync<PaymentData>("select cardnumber, currencycode, expirymonth, expiryyear, merchantid, statuscode from public.payment where id = @Id;", new { Id = paymentId });
                return paymentData;
            }
        }
    }
}
