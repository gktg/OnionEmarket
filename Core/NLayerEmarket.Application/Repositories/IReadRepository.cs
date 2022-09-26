using NLayerEmarket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEmarket.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true); // get all entities

        IQueryable<T> GetWhere(Expression<Func<T,bool>> method, bool tracking = true);

        Task<T> GetSingleAsync (Expression<Func<T, bool>> method, bool tracking = true); // get single entity

        Task<T> GetByIdAsync (string id, bool tracking = true);    
        
    }
}
