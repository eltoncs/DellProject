using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InsuranceServices.Application.Interfaces
{
    public interface IAppService<TEntity> : IDisposable
    {
        Task<TEntity> GetById(Guid id);
        Task<TEntity> Save(TEntity obj);
        //IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        Task Remove(Guid id);
    }
}