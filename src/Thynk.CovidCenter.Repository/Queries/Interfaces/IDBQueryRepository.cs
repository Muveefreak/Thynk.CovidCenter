using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Thynk.CovidCenter.Repository.Queries.Interfaces
{
    public interface IDBQueryRepository<TEntity> where TEntity : class
    {
        TEntity Find(object id);
        Task<TEntity> FindAsync(object id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);
        Task<IQueryable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity GetByIncludesAsync<T1>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, T1, TEntity> map, params Expression<Func<TEntity, object>>[] includes);
        TEntity GetByDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        bool IsExist(Expression<Func<TEntity, bool>> predicate);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
