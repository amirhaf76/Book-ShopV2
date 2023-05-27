using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IReservationService
    {
        Task<BookReservationResultDto> ReserveBookAsync(BookReservationDto aBook);

        Task<ReservedBookCancellationResultDto> CancelBookReservationAsync(ReservedBookCancellationDto aBook);
    }
}