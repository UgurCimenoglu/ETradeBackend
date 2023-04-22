using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Repositories.CompletedOrder;
using ETradeBackend.Persistance.Contexts;

namespace ETradeBackend.Persistance.Repositories.CompletedOrder
{
    public class CompletedOrderReadRepository : ReadRepository<Domain.Entities.CompletedOrder>, ICompletedOrderReadRepository
    {
        public CompletedOrderReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
