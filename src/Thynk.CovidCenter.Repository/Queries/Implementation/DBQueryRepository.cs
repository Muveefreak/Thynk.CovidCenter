using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Thynk.CovidCenter.Repository.Queries.Interfaces;

namespace Thynk.CovidCenter.Repository.Queries.Implementation
{
    public class DBQueryRepository<TEntity> : IDBQueryRepository<TEntity> where TEntity : class
    {
        private readonly CovidCenterDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public DBQueryRepository(CovidCenterDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Find(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable<TEntity>();
        }

        public async Task<IQueryable<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable<TEntity>();
        }

        public TEntity GetByDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public async Task<TEntity> GetByDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public TEntity GetByIncludesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var Query = GetAllIncluding(includes);

            return Query.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> Queryable = _dbSet.AsNoTracking();

            return includeProperties.Aggregate
              (Queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public IQueryable<TEntity> GetByAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> Queryable = _dbSet.AsNoTracking().Where(predicate);

            return includeProperties.Aggregate
              (Queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Any(predicate);
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate);
        }
    }
}
