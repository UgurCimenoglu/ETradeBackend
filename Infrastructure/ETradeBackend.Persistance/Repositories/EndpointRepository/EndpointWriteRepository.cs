using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETradeBackend.Application.Repositories.EndpointRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistance.Contexts;

namespace ETradeBackend.Persistance.Repositories.EndpointRepository
{
    public class EndpointWriteRepository : WriteRepository<Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
