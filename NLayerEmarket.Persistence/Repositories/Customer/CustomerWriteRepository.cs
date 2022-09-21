using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Domain.Entities;
using NLayerEmarket.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEmarket.Persistence.Repositories
{
    public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerrWriteRepository
    {
        public CustomerWriteRepository(NLayerEmarketDbContext context) : base(context)
        {
        }
    }
}
