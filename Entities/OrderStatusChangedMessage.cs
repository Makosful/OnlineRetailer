using System;
using System.Collections.Generic;

namespace Entities
{
    public class OrderStatusChangedMessage
    {
        public int CustomerId { get; set; }

        public List<OrderLine> Products { get; set; }
    }

    public class OrderLine
    {
        public string Name { get; set; }
    }
}