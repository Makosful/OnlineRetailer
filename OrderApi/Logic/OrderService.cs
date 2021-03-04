using System;
using System.Collections.Generic;
using System.Linq;
using OrderApi.Data.Abstractions;
using OrderApi.Logic.Abstractions;
using OrderApi.Models;

namespace OrderApi.Logic
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductRest _productRest;

        public OrderService(IOrderRepository orderRepository, IProductRest productRest,
            IProductOrderRepository productOrderRepository)
        {
            _orderRepository = orderRepository;
            _productRest = productRest;
            _productOrderRepository = productOrderRepository;
        }

        Order IOrderService.Get(int id)
        {
            var order = _orderRepository.Get(id);
            order.Products = new List<Product>();
            var productOrders = _productOrderRepository.GetAllFromOrder(id);

            foreach (var productOrder in productOrders)
            {
                var product = _productRest.GetProductById(productOrder.ProductId);
                product.Quantity = productOrder.Quantity;
                order.Products.Add(product);
            }

            return order;
        }

        Order IOrderService.Add(Order order)
        {
            List<Product> products = order.Products;
            
            // Check all products are available
            for (var i = 0; i < products.Count; i++)
            {
                var product = _productRest.GetProductById(products[i].Id);
                product.Quantity = products[i].Quantity;

                var availableStock = product.ItemsInStock - product.ItemsReserved;
                if (availableStock < product.Quantity)
                    throw new ArgumentException(
                        $"Product [{product.Name}] does not have sufficient available stock. Current available stock is [{availableStock}]");
                products[i] = product;
            }

            var batch = new List<ProductOrder>();
            foreach (var product in products)
            {
                batch.Add(new ProductOrder(){OrderId = order.Id, ProductId = product.Id, Quantity = product.Quantity});
            }

            var success = _productOrderRepository.AddBatch(batch);

            if (!success)
            {
                _orderRepository.Remove(order.Id);
                    throw new ArgumentException("Failed to add the new order.");
            }

            return order;
        }

        bool IOrderService.UpdateStatus(int orderId, OrderStatus orderStatus)
        {
            return _orderRepository.UpdateStatus(orderId, orderStatus);
        }

        IEnumerable<Order> IOrderService.GetAll()
        {
            var orders = _orderRepository.GetAll();

            var a = new List<Order>();
            foreach (var order in orders)
            {
                Order item = (this as IOrderService).Get(order.Id);
                a.Add(item);
            }

            return a;
        }
    }
}