using Microsoft.EntityFrameworkCore;
using Polly;
using Serilog;
using System;
using VehicleManagementAPI.Controllers.Model;

namespace VehicleManagementAPI.DataAccess
{
    public class VehicleManagementDBContext : DbContext
    {
        public VehicleManagementDBContext(DbContextOptions<VehicleManagementDBContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vehicle>().HasKey(m => m.LicenseNumber);
            builder.Entity<Vehicle>().ToTable("Vehicle");
            base.OnModelCreating(builder);
        }

        public void MigrateDB()
        {
            Log.Information("Ensure VehicleManagement Database");

            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10), (ex, ts) => { Log.Error("Error applying migrations. Retrying in 10 sec."); })
                .Execute(() => Database.Migrate());
        }
    }
}
