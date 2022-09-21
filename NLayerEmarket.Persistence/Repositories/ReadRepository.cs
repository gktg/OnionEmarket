using Microsoft.EntityFrameworkCore;
using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Domain.Entities.Common;
using NLayerEmarket.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEmarket.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly NLayerEmarketDbContext _context;

        public ReadRepository(NLayerEmarketDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            return Table.Where(method);
        }
        
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
        {
            return await Table.FirstOrDefaultAsync(method);
        }
        public async Task<T> GetByIdAsync(string id)
        {
            return await Table.FindAsync(Guid.Parse(id));
        }




    }
}
