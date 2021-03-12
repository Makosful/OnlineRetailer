using System;
using System.Threading;
using EasyNetQ;
using Entities;
using Microsoft.Extensions.Logging;
using ProductApi.Data.Abstractions;
using ProductApi.Models;

namespace ProductApi.Data
{
    public class RabbitMessaging: IDisposable
    {
        private const string ConnectionString = "host=192.168.1.177;username=guest;password=guest";
        private readonly IBus _bus;
        private readonly ILogger<RabbitMessaging> _logger;

        public RabbitMessaging(ILogger<RabbitMessaging> logger)
        {
            _logger = logger;
            _bus = RabbitHutch.CreateBus(ConnectionString);
        }
        
        public void Dispose()
        {
            _bus?.Dispose();
        }
        
        public void Listen()
        {
            _bus.PubSub.Subscribe<OrderStatusChangedMessage>("productApiMsCompleted", 
                HandleOrderStatusComplete,
                x => x.WithTopic("completed"));

            lock (this)
            {
                Monitor.Wait(this);
            }
        }

        private void HandleOrderStatusComplete(OrderStatusChangedMessage message)
        {
            foreach (var product in message.Products)
            {
                _logger.LogInformation("{ProductName}", product.Name);
            }
        }
    }
}