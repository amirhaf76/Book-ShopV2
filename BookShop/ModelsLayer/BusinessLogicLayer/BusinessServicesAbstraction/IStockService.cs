using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IStockService
    {
        Task<StockingBookResultDto> StockBookAsync(StockingBookDto stockingBookDto);

        Task<UpdatingStockedBookResultDto> UpdateStockAsync(UpdatingStockedBookDto stock);

        Task<IEnumerable<StockBookResultDto>> GetStockAsync(GettingStockBookFilter filter);

        Task<IEnumerable<StockBookResultDto>> GetStockAsync();

    }
}
