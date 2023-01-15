using ETradeBackend.Application.Repositories.InvoiceFileRepository;
using ETradeBackend.Domain.Entities;
using ETradeBackend.Persistance.Contexts;

namespace ETradeBackend.Persistance.Repositories.InvoiceFileRepository
{
    public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
