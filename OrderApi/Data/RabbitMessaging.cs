using System;
using System.Collections.Generic;
using System.Linq;
using EasyNetQ;
using OrderApi.Data.Abstractions;
using OrderApi.Models;

namespace OrderApi.Data
{
    public class RabbitMessaging: IMessagePublisher, IDisposable
    {
        private const string ConnectionString = "";
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
            var message = new OrderStatusChangedMessage()
            {
                CustomerId = 0,
                Products = products
            };
            
            _bus.PubSub.Publish(message, topic);
        }
    }
}