using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepository;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext dbContexts) : base(dbContexts)
        {
        }

        //public async Task<IEnumerable<Reservation>> GetReservationAsync()
        //{

        //}
    }

}
