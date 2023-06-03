using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IStockService
    {
        Task<StockingBookResultDto> StockBookAsync(StockingBookDto stockingBookDto);

        Task<StockStatusUpdateResultDto> UpdateStockStatus(StockStatusUpdateDto updateStockStatus);

        Task<StockReservationResultDto> ReserveStockWithCheckingAsync(StockReservationDto stockReservation);

        Task<UpdatingStockedBookResultDto> UpdateStockAsync(UpdatingStockedBookDto stock);

        Task<IEnumerable<StockBookResultDto>> GetStockAsync(GettingStockBookFilter filter);

        Task<IEnumerable<StockBookResultDto>> GetStockAsync();

    }
}
