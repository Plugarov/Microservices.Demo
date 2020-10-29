using System.Collections.Generic;
using System.Threading.Tasks;
using WorkshopManagementAPI.Repositories.Model;

namespace WorkshopManagementAPI.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerAsync(string customerId);

    }
}
