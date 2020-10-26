using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Commands;
using WebApp.Models;
using Refit;

namespace WebApp.RESTClients
{
    public interface ICustomerManagementAPI
    {
        [Get("/customers")]
        Task<List<Customer>> GetCustomers();

        [Get("/customers/{id}")]
        Task<Customer> GetCustomerById([AliasAs("id")] string customerId);

        [Post("/customers")]
        Task RegisterCustomer(RegisterCustomer command);
    }
}
