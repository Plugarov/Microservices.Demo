using System;
using System.Collections.Generic;

namespace WorkshopManagementAPI.DTOs
{
    public class WorkshopPlanningDTO
    {
        public DateTime Date { get; set; }
        public List<MaintenanceJobDTO> Jobs { get; set; }
    }
}
