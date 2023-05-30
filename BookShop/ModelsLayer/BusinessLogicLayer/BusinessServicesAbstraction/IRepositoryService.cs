using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IRepositoryService
    {
        Task<StockingBookResultDto> StockBookAsync(StockingBookDto stockingBookDto);

        Task<BookReductionResultDto> ReduceBookAsync(BookReductionDto aBook);

        Task<IEnumerable<StockBookResultDto>> GetStockBookAsync(GettingStockBookFilter filter);

        Task<IEnumerable<RepositoryResult>> GetRepositoriesAsync(GettingRepositoriesFilter filter);

        Task<IEnumerable<StockBookResultDto>> GetStockBookAsync();

        Task<IEnumerable<RepositoryResult>> GetRepositoriesAsync();

        Task<RecordingRepositoryResultDto> AddRepositoryAsync(RecordingRepositoryDto aRepository);

        Task<RemovalRespositoryResultDto> ChangeRepositoryActivation(RemovalRespositoryDto aRepository);
    }
}
