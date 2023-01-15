using ETradeBackend.Application.Repositories.InvoiceFileRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistance.Contexts;

namespace ETradeBackend.Persistance.Repositories.InvoiceFileRepository
{
    public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
