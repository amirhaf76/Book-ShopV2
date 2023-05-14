using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContexts;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext dbContexts)
        {
            _dbContexts = dbContexts;

            _dbSet = _dbContexts.Set<TEntity>();
        }

        public EntityEntry<TEntity> Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public EntityEntry<TEntity> Remove(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }

        public EntityEntry<TEntity> Update(TEntity entity)
        {
            return _dbSet.Update(entity);
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return await _dbSet.AddAsync(entity);
        }

        public async void AddRangeAsync(params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async void AddRangeAsync(IEnumerable<TEntity> entities )
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void AddRange(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContexts.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContexts.SaveChangesAsync();
        }

    }
}
