using Infrastructure.Messaging;
using System;

namespace WorkshopManagementAPI.Events
{
    public class WorkshopPlanningCreated : Event
    {
        public readonly DateTime Date;

        public WorkshopPlanningCreated(Guid messageId, DateTime date) : base(messageId)
        {
            Date = date;
        }
    }
}
