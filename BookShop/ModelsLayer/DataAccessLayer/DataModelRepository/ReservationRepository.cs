using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
using BookShop.ModelsLayer.DataAccessLayer.ExceptionModels;
using BookShop.ModelsLayer.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext dbContexts) : base(dbContexts)
        {
        }

        public  async Task<Reservation> GetReservationAsync(int id)
        {
            var theReservation = await FindAsync(id);

            await _dbSet.Entry(theReservation).Collection(x => x.Stocks).LoadAsync();
            await _dbSet.Entry(theReservation).Reference(x => x.UserAccount).LoadAsync();

            return theReservation;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            return await GetReservationsAsync(new ReservationFilter());
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync(ReservationFilter reservationFilter)
        {
            var queryable = GetReservationAsQueryable(reservationFilter);

            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationWithTheirStocksAsync()
        {
            return await GetReservationWithTheirStocksAsync(new ReservationFilter());
        }

        public async Task<IEnumerable<Reservation>> GetReservationWithTheirStocksAsync(ReservationFilter reservationFilter)
        {
            var queryable = GetReservationAsQueryable(reservationFilter);

            return await queryable
                .Select(q => new Reservation
                {
                    Id = q.Id,
                    LastChange = q.LastChange,
                    ComfirmationTime = q.ComfirmationTime,
                    Status = q.Status,
                    UserAccountId = q.UserAccountId,
                    UserAccount = q.UserAccount,
                    Stocks = q.Stocks,
                })
                .ToListAsync();
        }

        public async Task CancelBookReservationAsync(int id)
        {
            var theReservation = await FindAsync(id);

            if (theReservation == null)
            {
                throw new ReservedBookNotFoundException();
            }

            using var transaction = _dbContexts.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);

            try
            {
                await _dbSet.Entry(theReservation).Collection(r => r.Stocks).LoadAsync();

                foreach (var stock in theReservation.Stocks)
                {
                    stock.ReservationId = null;
                }

                await SaveChangesAsync();

                theReservation.Status = ReservationStatus.Canceled;

                await SaveChangesAsync();

                await transaction.CommitAsync();
            } 
            catch (Exception e)
            {
                throw new UnsuccessfulCancellingReservationException("There was a problem while cancelling a reservation!", e);
            }
        }



        private IQueryable<Reservation> GetReservationAsQueryable(ReservationFilter reservationFilter)
        {
            var queryable = _dbSet.AsQueryable();

            if (reservationFilter.Id != null)
            {
                queryable = queryable.Where(q => q.Id == reservationFilter.Id);
            }

            if (reservationFilter.LastChange != null)
            {
                queryable = reservationFilter.LastChange.AddStartTimeCondition(queryable, q => q.LastChange);
                queryable = reservationFilter.LastChange.AddEndTimeCondition(queryable, q => q.LastChange);
            }

            if (reservationFilter.ComfirmationTime != null)
            {
                queryable = reservationFilter.ComfirmationTime.AddStartTimeCondition(queryable, q => q.ComfirmationTime);
                queryable = reservationFilter.ComfirmationTime.AddEndTimeCondition(queryable, q => q.ComfirmationTime);
            }

            if (reservationFilter.Status != null)
            {
                queryable = queryable.Where(q => q.Status == reservationFilter.Status);
            }

            if (reservationFilter.UserAccountId != null)
            {
                queryable = queryable.Where(q => q.UserAccountId == reservationFilter.UserAccountId);
            }

            var sortedQuery = queryable.OrderByDescending(q => q.Id);

            queryable = reservationFilter.Pagination.AddPaginationTo(sortedQuery);
            return queryable;
        }
    }

}
