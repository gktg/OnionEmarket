using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayerEmarket.Domain.Entities;
using NLayerEmarket.Domain.Entities.Common;

namespace NLayerEmarket.Persistence.Contexts
{
    public class NLayerEmarketDbContext : DbContext
    {
        public NLayerEmarketDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categorys { get; set; }


    }
}
