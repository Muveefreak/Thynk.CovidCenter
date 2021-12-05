using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thynk.CovidCenter.Repository.Commands.Interfaces;

namespace Thynk.CovidCenter.Repository.Commands.Implementation
{
    public class DBCommandRepository<TEntity> : IDBCommandRepository<TEntity> where TEntity : class
    {
        private CovidCenterDbContext _context;
        private DbSet<TEntity> _dbSet;

        public DBCommandRepository(CovidCenterDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();

        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void AddRange(List<TEntity> entity)
        {
            _dbSet.AddRange(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                //throw new ArgumentNullException("entity");
            }
            _dbSet.Remove(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}
