using ETradeBackend.Application.Repositories;
using ETradeBackend.Domain.Entities.Common;
using ETradeBackend.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ETradeDbContext _context;

        public WriteRepository(ETradeDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry != null;
        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
            await Table.AddRangeAsync(model);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry != null;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var data = await Table.FirstOrDefaultAsync(q => q.Id == Guid.Parse(id));
            return Remove(data);
        }

        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry != null;
        }

        public bool UpdateRange(List<T> datas)
        {
            Table.UpdateRange(datas);
            return true;
        }

        public async Task<int> SaveAsync()
       => await _context.SaveChangesAsync();

    }
}
