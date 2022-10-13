using ETradeBackend.Application.Repositories.ProductRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Persistance.Repositories.ProductRepository
{
    public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
