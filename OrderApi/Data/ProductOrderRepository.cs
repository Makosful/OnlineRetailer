using System.Collections.Generic;
using System.Linq;
using OrderApi.Data.Abstractions;
using OrderApi.Models;

namespace OrderApi.Data
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        private readonly OrderApiContext _context;

        public ProductOrderRepository(OrderApiContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        IEnumerable<ProductOrder> IProductOrderRepository.GetAllFromOrder(int orderId)
        {
            var a = from b in _context.ProductOrders
                where b.OrderId == orderId
                select b;

            return a.ToList();
        }

        public bool AddBatch(IEnumerable<ProductOrder> productOrders)
        {
            _context.ProductOrders.AddRange(productOrders);
            var changes = _context.SaveChanges();
            return changes > 0;
        }
    }
}