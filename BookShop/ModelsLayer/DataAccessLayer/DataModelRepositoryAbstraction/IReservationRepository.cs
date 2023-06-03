using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationAsync();

        Task<IEnumerable<Reservation>> GetReservationAsync(ReservationFilter reservationFilter);

        Task<IEnumerable<Reservation>> GetReservationWithTheirStocksAsync();

        Task<IEnumerable<Reservation>> GetReservationWithTheirStocksAsync(ReservationFilter reservationFilter);

    }
}
