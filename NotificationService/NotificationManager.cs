using Infrastructure.Messaging;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using NotificationService.Events;
using NotificationService.Model;
using NotificationService.NotificationChannels;
using NotificationService.Repositories;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService
{
    public class NotificationManager : IHostedService, IMessageHandlerCallback
    {
        IMessageHandler _messageHandler;
        INotificationRepository _repo;
        IEmailNotifier _emailNotifier;

        public NotificationManager(IMessageHandler messageHandler, INotificationRepository repo, IEmailNotifier emailNotifier)
        {
            _messageHandler = messageHandler;
            _repo = repo;
            _emailNotifier = emailNotifier;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Start(this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Stop();
            return Task.CompletedTask;
        }

        public async Task<bool> HandleMessageAsync(string messageType, string message)
        {
            try
            {
                JObject messageObject = MessageSerializer.Deserialize(message);
                switch (messageType)
                {
                    case "CustomerRegistered":
                        await HandleAsync(messageObject.ToObject<CustomerRegistered>());
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while handling {messageType} event.");
            }

            return true;
        }

        private async Task HandleAsync(CustomerRegistered cr)
        {
            Customer customer = new Customer
            {
                CustomerId = cr.CustomerId,
                Name = cr.Name,
                TelephoneNumber = cr.TelephoneNumber,
                EmailAddress = cr.EmailAddress
            };

            Log.Information("Register customer: {Id}, {Name}, {TelephoneNumber}, {Email}",
                customer.CustomerId, customer.Name, customer.TelephoneNumber, customer.EmailAddress);

            await _repo.RegisterCustomerAsync(customer);
        }
    }
}
