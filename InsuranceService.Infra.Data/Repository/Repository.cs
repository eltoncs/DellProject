using InsuranceServices.Domain.Interfaces.Repositories;
using InsuranceServices.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InsuranceServices.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ISContext DbIS;
        protected DbSet<TEntity> DbSet;

        public Repository(ISContext context)
        {
            DbIS = context;
            DbSet = DbIS.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var ret = await DbSet.FindAsync(id);
            return ret;
        }

        /// <summary>
        /// Adds or updates one record
        /// </summary>
        /// <param name="obj">Entity to be saved/updated</param>
        /// <param name="id">PK value to identify the operation. Necessary because it uses generic entities</param>
        public virtual async Task<TEntity> Save(TEntity obj, Guid id)
        {
            var found = await GetById(id);

            if (found == null)
            {
                return DbSet.Add(obj);
            }

            DbIS.Entry(found).CurrentValues.SetValues(obj);
            return obj;
        }

        public async Task Remove(Guid id)
        {
            var found = await DbSet.FindAsync(id);
            if (found != null) DbSet.Remove(found);
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                var saved = await DbIS.SaveChangesAsync();
                return saved;
            }
            catch
            {
                return -1;
            }            
        }

        public void Dispose()
        {
            DbIS.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
