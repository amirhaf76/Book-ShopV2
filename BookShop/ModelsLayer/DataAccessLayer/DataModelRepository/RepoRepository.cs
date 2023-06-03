using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
using BookShop.ModelsLayer.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class RepoRepository : BaseRepository<Repository>, IRepoRepository
    {
        public RepoRepository(DbContext dbContexts) : base(dbContexts)
        {
        }

        public async Task<IEnumerable<Repository>> GetRepositoriesAsync()
        {
            return await GetRepositoriesAsync(new RepositoryFilter { });
        }

        public async Task<IEnumerable<Repository>> GetRepositoriesAsync(RepositoryFilter filter)
        {
            var queryable = GetRepositoriesWithoutEigerLoading(filter);

            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Repository>> GetRepositoriesWithTheirStocksAsync()
        {
            return await GetRepositoriesWithTheirStocksAsync(new RepositoryFilter { });
        }

        public async Task<IEnumerable<Repository>> GetRepositoriesWithTheirStocksAsync(RepositoryFilter filter)
        {
            var queryable = GetRepositoriesWithoutEigerLoading(filter);

            queryable = queryable.Include(r => r.Stocks);

            return await queryable.AsSplitQuery().ToListAsync();
        }

        public async Task<Repository> ChangeRepositoryActivationAsync(int id, bool isEnable)
        {
            var theRepository = await FindAsync(id);

            if (theRepository == null)
            {
                throw new RepositoryNotFoundException(id);
            }

            theRepository.IsEnable = isEnable;

            return theRepository;
        }

        private IQueryable<Repository> GetRepositoriesWithoutEigerLoading(RepositoryFilter filter)
        {
            var queryable = _dbSet.AsQueryable();

            if (filter.AddressId != null)
            {
                queryable = queryable.Where(q => q.AddressId == filter.AddressId);
            }

            if (filter.IsEnable != null)
            {
                queryable = queryable.Where(q => q.IsEnable == filter.IsEnable);
            }

            if (filter.Name != null)
            {
                queryable = queryable.Where(q => EF.Functions.Like(q.Name, $"%{filter.Name}%"));
            }

            if (filter.Id != null)
            {
                queryable = queryable.Where(q => q.Id == filter.Id);
            }

            var sortedQuery = queryable.OrderByDescending(q => q.Id);

            queryable = filter.Pagination.AddPaginationTo(sortedQuery);

            return queryable;
        }
    }
}
