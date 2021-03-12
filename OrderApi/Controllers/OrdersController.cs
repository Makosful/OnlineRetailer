using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using OrderApi.Data.Abstractions;
using OrderApi.Logic.Abstractions;
using OrderApi.Models;
using RestSharp;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMessagePublisher _publisher;

        public OrdersController(IOrderService orderService, IMessagePublisher publisher)
        {
            _orderService = orderService;
            _publisher = publisher;
        }

        // GET: orders
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _orderService.GetAll();
        }

        // GET orders/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var order = _orderService.Get(id);
            if (order == null) return NotFound();
            return new ObjectResult(order);
        }

        // POST orders
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (order == null) 
                return BadRequest("Order must be submitted in the request body");
            if (order.Products == null || order.Products.Count < 1)
                return BadRequest("An order must contain at least one product");

            try
            {
                var o = _orderService.Add(order);

                return Created($"orders/{o.Id}", o);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{orderId}/status")]
        public IActionResult UpdateOrderStatus(int orderId, [FromBody] string status)
        {
            var parse = Enum.TryParse(status, out OrderStatus orderStatus);
            
            if (!parse) return BadRequest($"Unknown status. Request rejected");

            //bool updated = _orderService.UpdateStatus(orderId, orderStatus);
            const bool updated = true;

            Order order = _orderService.Get(orderId);
            _publisher.PublishOrderStatusChangedMessage(order.Products, "completed");

            if (updated)
            {
                return Ok($"Status set to [{orderStatus}]");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}