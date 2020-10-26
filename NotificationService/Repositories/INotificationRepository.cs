using NotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Repositories
{
    public interface INotificationRepository
    {
        Task RegisterCustomerAsync(Customer customer);
        Task<Customer> GetCustomerAsync(string customerId);
    }
}
