using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IScope where TEntity : class
    {
        private readonly DbContext _dbContexts;
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


        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }


        public TEntity FindAndLoadProperties<TProperty>(
            Expression<Func<TEntity, IEnumerable<TProperty>>> includeProperties,
            params object[] keyValues) where TProperty : class
        {
            var receivedEntity = Find(keyValues);

            if (receivedEntity == null)
            {
                return null;
            }

            var entry = _dbSet.Entry(receivedEntity);

            entry.Collection(includeProperties).Load();

            return receivedEntity;
        }

        public async Task<TEntity> FindAndLoadPropertiesAsync<TProperty>(
            Expression<Func<TEntity, IEnumerable<TProperty>>> includeProperties,
            params object[] keyValues) where TProperty : class
        {
            var receivedEntity = await FindAsync(keyValues);

            if (receivedEntity == null)
            {
                return null;
            }

            var entry = _dbSet.Entry(receivedEntity);

            await entry.Collection(includeProperties).LoadAsync();

            return receivedEntity;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _dbContexts.Dispose();
        }
    }
}
