using ETradeBackend.Application.Repositories.Files;
using ETradeBackend.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Persistance.Repositories.FileRepository
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
