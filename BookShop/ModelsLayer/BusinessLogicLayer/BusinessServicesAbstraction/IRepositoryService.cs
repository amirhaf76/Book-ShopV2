using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IRepositoryService
    {
        Task<StockingBookResultDto> StockBookAsync(StockingBookDto stockingBookDto);
        Task<BookReductionResultDto> ReduceBookAsync(BookReductionDto aBook);
        Task<StockBooksResultDto> GetStockBookAsync(GettingStockBookFilter filter = null);
    }
}
