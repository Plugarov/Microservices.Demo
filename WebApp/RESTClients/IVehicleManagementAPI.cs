using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Commands;
using WebApp.Models;

namespace WebApp.RESTClients
{
    public interface IVehicleManagementAPI
    {
        [Get("/vehicles")]
        Task<List<Vehicle>> GetVehicles();

        [Get("/vehicles/{id}")]
        Task<Vehicle> GetVehicleByLicenseNumber([AliasAs("id")] string licenseNumber);

        [Post("/vehicles")]
        Task RegisterVehicle(RegisterVehicle command);
    }
}
