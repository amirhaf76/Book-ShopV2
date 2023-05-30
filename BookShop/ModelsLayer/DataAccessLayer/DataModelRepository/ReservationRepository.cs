using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext dbContexts) : base(dbContexts)
        {
        }

        public async Task<IEnumerable<Reservation>> GetReservationAsync()
        {
            return await GetReservationAsync(new ReservationFilter());
        }

        public async Task<IEnumerable<Reservation>> GetReservationAsync(ReservationFilter reservationFilter)
        {
            var queryable = _dbSet.AsQueryable();

            if(reservationFilter.Id != null)
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

            return await queryable.ToListAsync();
        }
    }

}
