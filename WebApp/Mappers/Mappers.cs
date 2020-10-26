using System;
using WebApp.Commands;
using WebApp.ViewModels;

namespace WebApp.Mappers
{
    public static class Mappers
    {
        public static RegisterCustomer MapToRegisterCustomer(this CustomerManagementNewViewModel source) => new RegisterCustomer
        (
            Guid.NewGuid(),
            Guid.NewGuid().ToString("N"),
            source.Customer.Name,
            source.Customer.Address,
            source.Customer.PostalCode,
            source.Customer.City,
            source.Customer.TelephoneNumber,
            source.Customer.EmailAddress
        );
    }
}
