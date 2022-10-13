using ETradeBackend.Application.Abstracts;
using ETradeBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Persistance.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()

            => new()
            {
                new()
                {
                    Id= Guid.NewGuid(),
                    Name="Laptop",
                    Price=10000,
                    CreatedDate= DateTime.Now,
                    Stock=10
                },
                new()
                {
                    Id= Guid.NewGuid(),
                    Name="Laptop",
                    Price=10000,
                    CreatedDate= DateTime.Now,
                    Stock=20
                },
                new()
                {
                    Id= Guid.NewGuid(),
                    Name="Laptop",
                    Price=10000,
                    CreatedDate= DateTime.Now,
                    Stock=30
                },
                new()
                {
                    Id= Guid.NewGuid(),
                    Name="Laptop",
                    Price=10000,
                    CreatedDate= DateTime.Now,
                    Stock=40
                }
            };

    }
}
