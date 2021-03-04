using System.Collections;
using System.Collections.Generic;
using OrderApi.Models;

namespace OrderApi.Data.Abstractions
{
    public interface IOrderRepository
    {
        Order Get(int id);
        Order Add(Order order);
        void Remove(int entityId);
        bool UpdateStatus(int orderId, OrderStatus orderStatus);
        IEnumerable<Order> GetAll();
    }
}