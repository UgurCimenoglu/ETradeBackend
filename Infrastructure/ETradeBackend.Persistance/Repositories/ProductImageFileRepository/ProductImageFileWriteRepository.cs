using ETradeBackend.Application.Repositories.ProductImageFileRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Persistance.Repositories.ProductImageFileRepository
{
    public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
