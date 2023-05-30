using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepository;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(DbContext dbContexts) : base(dbContexts)
        {
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync()
        {
            return await GetStocksAsync(new StockFilter());
        }

        public async Task<IEnumerable<Stock>> GetStocksAsync(StockFilter stockFilter)
        {
            var queryable = _dbSet.AsQueryable();

            if (stockFilter.RepositoryIds.Any())
            {
                queryable = queryable.Where(q => stockFilter.RepositoryIds.Contains(q.RepositoryId));
            }

            if (stockFilter.BookIds.Any())
            {
                queryable = queryable.Where(q => stockFilter.BookIds.Contains(q.BookId));
            }

            if (stockFilter.ReservationIds.Any())
            {
                queryable = queryable.Where(q => stockFilter.ReservationIds.Contains(q.ReservationId ?? 0));
            }

            if (stockFilter.Statuses.Any())
            {
                queryable = queryable.Where(q => stockFilter.Statuses.Contains(q.Status));
            }

            if (stockFilter.StockIds.Any())
            {
                queryable = queryable.Where(q => stockFilter.StockIds.Contains(q.StockId));
            }

            var sortedQueryable = queryable.OrderByDescending(s => s.StockId);

            queryable = stockFilter.Pagination.AddPaginationTo(sortedQueryable);

            return await queryable.ToListAsync();
        }

        
    }

}
