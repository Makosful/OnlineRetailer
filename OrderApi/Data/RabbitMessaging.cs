using System;
using System.Collections.Generic;
using System.Linq;
using EasyNetQ;
using Entities;
using OrderApi.Data.Abstractions;
using OrderApi.Models;

namespace OrderApi.Data
{
    public class RabbitMessaging: IMessagePublisher, IDisposable
    {
        private const string ConnectionString = "host=192.168.1.177;username=guest;password=guest";
        private readonly IBus _bus;

        public RabbitMessaging()
        {
            _bus = RabbitHutch.CreateBus(ConnectionString);
        }

        public void Dispose()
        {
            _bus?.Dispose();
        }

        void IMessagePublisher.PublishOrderStatusChangedMessage(List<Product> products, string topic)
        {
            var orderLines = products.Select(product => new OrderLine() {Name = product.Name}).ToList();
            var message = new OrderStatusChangedMessage()
            {
                CustomerId = 0,
                Products = orderLines
            };
            
            _bus.PubSub.Publish(message, topic);
        }
    }
}