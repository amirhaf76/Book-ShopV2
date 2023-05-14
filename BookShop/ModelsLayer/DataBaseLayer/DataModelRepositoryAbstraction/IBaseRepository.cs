using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);
        Task<TEntity> FindAsync(params object[] keyValues);
        TEntity Find(params object[] keyValues);
        EntityEntry<TEntity> Add(TEntity entity);
        EntityEntry<TEntity> Remove(TEntity entity);
        EntityEntry<TEntity> Update(TEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync();

        void AddRangeAsync(params TEntity[] entities);
        void AddRangeAsync(IEnumerable<TEntity> entities);
        void AddRange(params TEntity[] entities);
        void AddRange(IEnumerable<TEntity> entities);

    }
}