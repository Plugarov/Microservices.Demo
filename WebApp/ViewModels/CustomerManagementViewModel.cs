using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class CustomerManagementViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
    }
}
