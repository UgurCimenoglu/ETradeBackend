using ETradeBackend.Application.Repositories.OrderRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Persistance.Repositories.OrderRepository
{
    public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
