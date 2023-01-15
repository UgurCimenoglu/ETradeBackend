using ETradeBackend.Application.Repositories.ProductImageFileRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistance.Contexts;

namespace ETradeBackend.Persistance.Repositories.ProductImageFileRepository
{
    internal class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
