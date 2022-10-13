using ETradeBackend.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETradeDbContext>
    {
        public ETradeDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ETradeDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
