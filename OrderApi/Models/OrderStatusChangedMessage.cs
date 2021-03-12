using System.Collections.Generic;

namespace OrderApi.Models
{
    public class OrderStatusChangedMessage
    {
        public int CustomerId { get; set; }

        public List<Product> Products { get; set; }
    }
}