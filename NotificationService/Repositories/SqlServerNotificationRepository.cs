using Dapper;
using NotificationService.Model;
using Polly;
using Serilog;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NotificationService.Repositories
{
    public class SqlServerNotificationRepository : INotificationRepository
    {
        private string _connectionString;

        public SqlServerNotificationRepository(string connectionString)
        {
            _connectionString = connectionString;

            // init db
            Log.Information("Initialize Database");

            Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(10, r => TimeSpan.FromSeconds(10), (ex, ts) => { Log.Error("Error connecting to DB. Retrying in 10 sec."); })
            .ExecuteAsync(InitializeDB)
            .Wait();
        }

        private async Task InitializeDB()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString.Replace("Notification", "master")))
            {
                await conn.OpenAsync();

                // create database
                string sql =
                    "IF DB_ID('Notification') IS NULL CREATE DATABASE Notification;";

                await conn.ExecuteAsync(sql);

                // create tables
                conn.ChangeDatabase("Notification");

                sql = "IF OBJECT_ID('Customer') IS NULL " +
                      "CREATE TABLE Customer (" +
                      "  CustomerId varchar(50) NOT NULL," +
                      "  Name varchar(50) NOT NULL," +
                      "  TelephoneNumber varchar(50)," +
                      "  EmailAddress varchar(50)," +
                      "  PRIMARY KEY(CustomerId));";

                await conn.ExecuteAsync(sql);
            }
        }

        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryFirstOrDefaultAsync<Customer>("select * from Customer where CustomerId = @CustomerId",
                    new { CustomerId = customerId });
            }
        }

        public async Task RegisterCustomerAsync(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql =
                    "insert into Customer(CustomerId, Name, TelephoneNumber, EmailAddress) " +
                    "values(@CustomerId, @Name, @TelephoneNumber, @EmailAddress);";
                await conn.ExecuteAsync(sql, customer);
            }
        }
    }
}
