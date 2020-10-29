
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkshopManagementAPI.Repositories.Model;

namespace WorkshopManagementAPI.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> GetVehicleAsync(string licenseNumber);
    }
}
