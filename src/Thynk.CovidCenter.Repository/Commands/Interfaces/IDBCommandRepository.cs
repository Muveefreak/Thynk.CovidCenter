using System.Collections.Generic;
using System.Threading.Tasks;

namespace Thynk.CovidCenter.Repository.Commands.Interfaces
{
    public interface IDBCommandRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(List<TEntity> entity);
        Task AddRangeAsync(List<TEntity> entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        int Save();
        Task<int> SaveAsync();
    }
}
