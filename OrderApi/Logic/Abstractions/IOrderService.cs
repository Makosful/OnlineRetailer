using System.Collections.Generic;
using OrderApi.Models;

namespace OrderApi.Logic.Abstractions
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order Get(int id);
        Order Add(Order order);
        bool UpdateStatus(int orderId, OrderStatus orderStatus);
    }
}