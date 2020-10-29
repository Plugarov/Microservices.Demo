using System;
using System.Threading.Tasks;
using WorkshopManagementAPI.Commands;
using WorkshopManagementAPI.Domain.Entities;

namespace WorkshopManagementAPI.CommandHandlers
{
    public interface IFinishMaintenanceJobCommandHandler
    {
        Task<WorkshopPlanning> HandleCommandAsync(DateTime planningDate, FinishMaintenanceJob command);
    }
}
