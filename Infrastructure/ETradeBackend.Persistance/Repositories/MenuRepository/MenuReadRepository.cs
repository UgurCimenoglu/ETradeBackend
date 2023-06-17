using ETradeBackend.Application.Repositories.MenuRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistance.Contexts;

namespace ETradeBackend.Persistance.Repositories.MenuRepository
{
    public class MenuReadRepository : ReadRepository<Menu>, IMenuReadRepository
    {
        public MenuReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
