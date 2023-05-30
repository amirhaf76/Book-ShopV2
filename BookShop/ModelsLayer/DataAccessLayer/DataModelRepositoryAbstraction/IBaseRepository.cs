using System.Linq.Expressions;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Find(params object[] keyValues);

        TEntity Add(TEntity entity);

        TEntity Remove(TEntity entity);

        TEntity Update(TEntity entity);


        void AddRange(params TEntity[] entities);

        void AddRange(IEnumerable<TEntity> entities);


        Task<TEntity> FindAsync(params object[] keyValues);

        Task<TEntity> AddAsync(TEntity entity);


        Task AddRangeAsync(params TEntity[] entities);

        Task AddRangeAsync(IEnumerable<TEntity> entities);


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync();
    }
}