using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class VehicleManagementViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}
