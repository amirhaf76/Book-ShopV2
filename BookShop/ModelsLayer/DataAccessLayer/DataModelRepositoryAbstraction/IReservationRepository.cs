using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();

        Task<IEnumerable<Reservation>> GetReservationsAsync(ReservationFilter reservationFilter);

        Task<Reservation> GetReservationAsync(int id);

        Task<IEnumerable<Reservation>> GetReservationWithTheirStocksAsync();

        Task<IEnumerable<Reservation>> GetReservationWithTheirStocksAsync(ReservationFilter reservationFilter);

    }
}
