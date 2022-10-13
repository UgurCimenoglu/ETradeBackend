using ETradeBackend.Application.Repositories.CustomerRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Persistance.Repositories.CustomerRepository
{
    public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
