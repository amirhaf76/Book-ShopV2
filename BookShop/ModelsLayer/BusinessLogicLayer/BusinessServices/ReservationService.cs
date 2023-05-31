using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class ReservationService : IReservationService, IScope
    {
        public Task<ReservedBookCancellationResultDto> CancelBookReservationAsync(ReservedBookCancellationDto aBook)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookReservationReportDto>> GetReservedBookAsync(object filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<BookReservationResultDto> ReserveBookAsync(BookReservationDto aBook)
        {
            throw new NotImplementedException();
        }
    }
}
