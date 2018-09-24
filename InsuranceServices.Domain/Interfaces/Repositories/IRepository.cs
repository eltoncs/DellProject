using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InsuranceServices.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task <TEntity> GetById(Guid id);
        Task<TEntity> Save(TEntity obj, Guid id);
        Task Remove(Guid id);
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}