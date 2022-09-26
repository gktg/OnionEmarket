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
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        public UserReadRepository(NLayerEmarketDbContext context) : base(context)
        {
        }
    }
}
