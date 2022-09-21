using NLayerEmarket.Application.Repositories;
using NLayerEmarket.Persistence.Contexts;
using NLayerEmarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEmarket.Persistence.Repositories
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(NLayerEmarketDbContext context) : base(context)
        {
        }
    }
}
