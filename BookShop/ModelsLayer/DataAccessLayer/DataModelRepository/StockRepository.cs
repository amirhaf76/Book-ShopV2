﻿using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
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
            IQueryable<Stock> queryable = GetStocksWithoutLoading(stockFilter);

            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetStocksCompletelyAsync()
        {
            return await GetStocksCompletelyAsync(new StockFilter());
        }

        public async Task<IEnumerable<Stock>> GetStocksCompletelyAsync(StockFilter stockFilter)
        {
            IQueryable<Stock> queryable = GetStocksWithoutLoading(stockFilter);

            return await queryable.Select(s => new Stock
            {
                StockId = s.StockId,
                RepositoryId = s.RepositoryId,
                Repository = s.Repository,
                BookId = s.BookId,
                Book = s.Book,
                ReservationId = s.ReservationId,
                Reservation = s.Reservation,
                Status = s.Status,
            }).ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetReservedStocksCompletelyAsync(ReservedStockFilter stockFilter)
        {
            var queryable = GetReservedStocksWithoutLoading(stockFilter);

            return await queryable.Select(s => new Stock
            {
                StockId = s.StockId,
                RepositoryId = s.RepositoryId,
                Repository = s.Repository,
                BookId = s.BookId,
                Book = s.Book,
                ReservationId = s.ReservationId,
                Reservation = s.Reservation,
                Status = s.Status,
            }).ToListAsync();
        }

        public async Task<IEnumerable<Stock>> GetReservedStocksAsync(ReservedStockFilter stockFilter)
        {
            var queryable = GetReservedStocksWithoutLoading(stockFilter);

            return await queryable.Select(s => new Stock
            {
                StockId = s.StockId,
                RepositoryId = s.RepositoryId,
                Repository = s.Repository,
                BookId = s.BookId,
                Book = s.Book,
                ReservationId = s.ReservationId,
                Reservation = s.Reservation,
                Status = s.Status,
            }).ToListAsync();
        }


        private IQueryable<Stock> GetStocksWithoutLoading(StockFilter stockFilter)
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
                queryable = queryable.Where(q => stockFilter.ReservationIds.Contains(q.ReservationId ?? default));
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

            return queryable;
        }

        private IQueryable<Stock> GetReservedStocksWithoutLoading(ReservedStockFilter stockFilter)
        {
            var queryable = _dbSet.AsQueryable().Where(q => q.ReservationId != null);

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
                queryable = queryable.Where(q => stockFilter.ReservationIds.Contains(q.ReservationId ?? default));
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

            return queryable;
        }

    }

}
