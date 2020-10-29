using System;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class WorkshopManagementViewModel
    {
        public DateTime Date { get; set; }
        public List<MaintenanceJob> MaintenanceJobs { get; set; }
    }
}
