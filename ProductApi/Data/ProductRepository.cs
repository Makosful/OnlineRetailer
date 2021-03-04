using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProductApi.Models;

namespace ProductApi.Data
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ProductApiContext _context;
        private readonly List<Product> _products;
        
        public ProductRepository(ProductApiContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();

            _products = new List<Product>()
            {
                new Product() {
                    Id = 1, Name = "Generic item",
                    Price = 5.00M, 
                    ItemsInStock = 100,
                    ItemsReserved = 5
                },
                new Product()
                {
                    Id = 2,
                    Name = "Generic but expensive item",
                    Price = 6.00M,
                    ItemsInStock =  80,
                    ItemsReserved = 3
                },
                new Product()
                {
                    Id = 3, 
                    Name = "Rare version of generic item",
                    Price = 10.00M,
                    ItemsInStock =  60,
                    ItemsReserved = 1
                },
            };
        }

        Product IRepository<Product>.Add(Product entity)
        {
            return entity;
        }

        void IRepository<Product>.Edit(Product entity)
        {
            // Do nothing
        }

        Product IRepository<Product>.Get(int id)
        {
            switch (id)
            {
                case 1:
                    return _products[0];
                case 2:
                    return _products[1];
                case 3:
                    return _products[2];
                default:
                    return null;
            }
        }

        IEnumerable<Product> IRepository<Product>.GetAll()
        {
            return _products;
        }

        void IRepository<Product>.Remove(int id)
        {
            // Do nothing for now
        }
    }
}
