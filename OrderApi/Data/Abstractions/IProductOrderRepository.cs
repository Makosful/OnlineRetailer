using System.Collections.Generic;
using OrderApi.Models;

namespace OrderApi.Data.Abstractions
{
    public interface IProductOrderRepository
    {
        IEnumerable<ProductOrder> GetAllFromOrder(int orderId);
        
        bool AddBatch(IEnumerable<ProductOrder> productOrders);
    }
}