using CustomerManagementAPI.Model;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using Serilog;

namespace CustomerManagementAPI.DataAccess
{
    public class CustomerManagementDbContext : DbContext
    {
        public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasKey(m => m.CustomerId);
            base.OnModelCreating(builder);
        }

        public void MigrateDB()
        {
            Log.Information("Ensure CustomerManagement Database");

            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10), (ex, ts) => { Log.Error("Error applying migrations. Retrying in 10 sec."); })
                .Execute(() =>  Database.Migrate());
        }
    }
}
