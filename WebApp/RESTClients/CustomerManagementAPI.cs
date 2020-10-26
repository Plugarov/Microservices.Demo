using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Commands;
using WebApp.Models;

namespace WebApp.RESTClients
{
    public class CustomerManagementAPI : ICustomerManagementAPI
    {
        private ICustomerManagementAPI _restClient;

        public CustomerManagementAPI(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("CustomerManagementAPI");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
            _restClient = RestService.For<ICustomerManagementAPI>(httpClient);
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _restClient.GetCustomers();
        }

        public async Task<Customer> GetCustomerById([AliasAs("id")] string customerId)
        {
            try
            {
                return await _restClient.GetCustomerById(customerId);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task RegisterCustomer(RegisterCustomer command)
        {
            await _restClient.RegisterCustomer(command);
        }
    }
}
