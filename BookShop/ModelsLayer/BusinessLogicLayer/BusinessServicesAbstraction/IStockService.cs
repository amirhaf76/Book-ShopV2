using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IStockService
    {
        Task<StockingBookResultDto> StockBookAsync(StockingBookDto stockingBookDto);

        Task<StockStatusUpdateResultDto> UpdateStockStatus(StockStatusUpdateDto updateStockStatus);

        Task<StockReservationResultDto> ReserveStockAsync(StockReservationDto stockReservation);

        Task<StockReservationResultDto> ReserveStockAsync(IEnumerable<int> bookIds, int reservationId);

        Task<ReservationCancellationResultDto> CancelStocksAsync(IEnumerable<long> stockIds);

        Task<UpdatingStockedBookResultDto> UpdateStockAsync(UpdatingStockedBookDto stock);

        Task<IEnumerable<StockBookResultDto>> GetStockAsync(GettingStockBookFilter filter);

        Task<IEnumerable<StockBookResultDto>> GetStockAsync();

    }
}
