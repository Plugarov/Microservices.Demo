using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementAPI.DataAccess;
using CustomerManagementAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastructure.Messaging;
using RabbitMQ.Client;
using CustomerManagementAPI.Events;
using CustomerManagementAPI.Commands;
using CustomerManagementAPI.Mappers;
using System;
using Microsoft.AspNetCore.Http;

namespace CustomerManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomersController : ControllerBase
    {
        IMessagePublisher _messagePublisher;
        private readonly CustomerManagementDbContext _dbContext;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger,
            CustomerManagementDbContext dbContext, IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _dbContext = dbContext;
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            var customers = await _dbContext.Customers.ToListAsync();



            return customers.Select(x => new Customer
            {
                CustomerId = x.CustomerId,
                Address = x.Address,
                City = x.City,
                EmailAddress = x.EmailAddress,
                Name = x.Name,
                PostalCode = x.PostalCode,
                TelephoneNumber = x.TelephoneNumber
            });
        }

        [HttpGet]
        [Route("{customerId}", Name = "GetByCustomerId")]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterCustomer command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = command.MapToCustomer();
                    _dbContext.Customers.Add(customer);
                    await _dbContext.SaveChangesAsync();

                    CustomerRegistered e = command.MapToCustomerRegistered();
                    await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                    return CreatedAtRoute("GetByCustomerId", new { customerId = customer.CustomerId }, customer);
                }
                return BadRequest();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
