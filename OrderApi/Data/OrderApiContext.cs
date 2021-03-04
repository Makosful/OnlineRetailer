using System;
using Microsoft.EntityFrameworkCore;
using OrderApi.Models;

namespace OrderApi.Data
{
    public class OrderApiContext : DbContext
    {
        public OrderApiContext(DbContextOptions<OrderApiContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("OrdersDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Ignore(x => x.Products);

            modelBuilder.Entity<ProductOrder>()
                .HasKey(x => new {x.OrderId, x.ProductId});

            modelBuilder.Entity<Order>().HasData(
                new Order {Id = 1, Date = DateTime.Now.AddDays(-7), Status = OrderStatus.Completed},
                new Order {Id = 2, Date = DateTime.Now.AddDays(-1), Status = OrderStatus.Cancelled},
                new Order {Id = 3, Date = DateTime.Now.AddHours(-20), Status = OrderStatus.Paid},
                new Order {Id = 4, Date = DateTime.Now, Status = OrderStatus.Shipped}
            );

            modelBuilder.Entity<ProductOrder>().HasData(
                new ProductOrder {OrderId = 1, ProductId = 1, Quantity = 3},
                new ProductOrder {OrderId = 1, ProductId = 2, Quantity = 2},
                new ProductOrder {OrderId = 1, ProductId = 3, Quantity = 5},
                new ProductOrder {OrderId = 2, ProductId = 3, Quantity = 1},
                new ProductOrder {OrderId = 3, ProductId = 3, Quantity = 2},
                new ProductOrder {OrderId = 4, ProductId = 2, Quantity = 1}
            );
        }
    }
}