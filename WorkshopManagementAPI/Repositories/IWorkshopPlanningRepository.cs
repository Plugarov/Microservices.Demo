using Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkshopManagementAPI.Domain.Entities;

namespace WorkshopManagementAPI.Repositories
{
    public interface IWorkshopPlanningRepository
    {
        void EnsureDatabase();
        Task<WorkshopPlanning> GetWorkshopPlanningAsync(DateTime date);
        Task SaveWorkshopPlanningAsync(string planningId, int originalVersion, int newVersion, IEnumerable<Event> newEvents);
    }
}
