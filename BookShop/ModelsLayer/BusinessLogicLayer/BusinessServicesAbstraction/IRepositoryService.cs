using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IRepositoryService
    {
        Task<StockingBookResultDto> StockBookAsync(StockingBookDto stockingBookDto);
        Task<BookReductionResultDto> ReduceBookAsync(BookReductionDto aBook);
        Task<StockBooksResultDto> GetStockBookAsync(GettingStockBookFilter filter);
        Task<GettingRepositoriesResult> GetRepositoriesAsync(GettingRepositoriesFilter filter);

        Task<StockBooksResultDto> GetStockBookAsync();
        Task<GettingRepositoriesResult> GetRepositoriesAsync();

        Task<RecordingRepositoryResultDto> AddRepositoryAsync(RecordingRepositoryDto aRepository);
        Task<RemovalRespositoryResultDto> RemoveRepositoryAsync(RemovalRespositoryDto aRepository);
    }
}
