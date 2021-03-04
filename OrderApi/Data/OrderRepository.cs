using System;
using System.Collections.Generic;
using System.Linq;
using OrderApi.Data.Abstractions;
using OrderApi.Models;

namespace OrderApi.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderApiContext _context;

        public OrderRepository(OrderApiContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        Order IOrderRepository.Get(int id)
        {
            var orderQuery = from orders in _context.Orders
                where orders.Id == id
                select orders;
            var order = orderQuery.ToList()[0];

            return order;
        }

        Order IOrderRepository.Add(Order order)
        {
            if (order.Date == null)
                order.Date = DateTime.Now;

            var newOrder = _context.Orders.Add(order).Entity;
            _context.SaveChanges();
            return newOrder;
        }

        void IOrderRepository.Remove(int id)
        {
            var order = _context.Orders.FirstOrDefault(p => p.Id == id);
            _context.Orders.Remove(order!);
            _context.SaveChanges();
        }

        bool IOrderRepository.UpdateStatus(int orderId, OrderStatus orderStatus)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null) return false;

            order.Status = orderStatus;
            _context.Orders.Update(order);

            var changes = _context.SaveChanges();
            return changes > 0;
        }

        IEnumerable<Order> IOrderRepository.GetAll()
        {
            var orderQuery = from orders in _context.Orders
                select orders;

            return orderQuery.ToList();
        }
    }
}