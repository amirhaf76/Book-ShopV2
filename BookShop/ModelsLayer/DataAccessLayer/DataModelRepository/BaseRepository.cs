using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IScope where TEntity : class
    {
        protected readonly DbContext _dbContexts;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext dbContexts)
        {
            _dbContexts = dbContexts;

            _dbSet = _dbContexts.Set<TEntity>();
        }


        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public TEntity Remove(TEntity entity)
        {
            return _dbSet.Remove(entity).Entity;
        }

        public TEntity Update(TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
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

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public async Task AddRangeAsync(params TEntity[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }


        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContexts.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContexts.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _dbContexts.Dispose();
        }
    }
}
