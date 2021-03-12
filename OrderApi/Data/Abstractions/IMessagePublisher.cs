using System.Collections.Generic;
using OrderApi.Models;

namespace OrderApi.Data.Abstractions
{
    public interface IMessagePublisher
    {
        void PublishOrderStatusChangedMessage(List<Product> products, string topic);
    }
}