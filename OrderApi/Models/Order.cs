using System;
using System.Collections.Generic;

namespace OrderApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public OrderStatus Status { get; set; }
        public List<Product> Products { get; set; }
    }
}