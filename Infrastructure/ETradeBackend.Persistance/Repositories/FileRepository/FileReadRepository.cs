using ETradeBackend.Application.Repositories.Files;
using ETradeBackend.Persistance.Contexts;

namespace ETradeBackend.Persistance.Repositories.FileRepository
{
    public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
