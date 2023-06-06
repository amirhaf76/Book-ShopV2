using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IReservationService
    {
        Task<BookReservationResultDto> ReserveBookAsync(BookReservationDto aBook);

        Task<ReservedBookCancellationResultDto> CancelBookReservationAsync(ReservedBookCancellationDto aBook);

        Task<IEnumerable<BookReservationReportDto>> GetReservedBookAsync(ReservedBookFilterDto filter);
    }
}