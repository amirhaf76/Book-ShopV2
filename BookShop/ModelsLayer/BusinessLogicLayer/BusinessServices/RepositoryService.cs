using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class RepositoryService : IRepositoryService, IScope
    {
        private readonly IRepoRepository _repoRepository;

        public RepositoryService(IRepoRepository repoRepository)
        {
            _repoRepository = repoRepository;
        }

        public async Task<RecordingRepositoryResultDto> AddRepositoryAsync(RecordingRepositoryDto aRepository)
        {
            var newRepository = new Repository
            {
                IsEnable = aRepository.IsEnable,
                Name =aRepository.Name
            };

            var addedRepository = await _repoRepository.AddAsync(newRepository);

            await _repoRepository.SaveChangesAsync();

            return new RecordingRepositoryResultDto
            {
                Id = addedRepository.Id,
                Name = addedRepository.Name,
                IsEnable = addedRepository.IsEnable,
            };
        }

        public async Task<IEnumerable<RepositoryResult>> GetRepositoriesAsync(GettingRepositoriesFilter filter)
        {
            var receivedStocks = await _repoRepository.GetRepositoriesAsync(filter.ConvertToRepositoryFilter());

            return receivedStocks.Select(r => r.ConvertToRepositoryResult());
        }

        public async Task<IEnumerable<RepositoryResult>> GetRepositoriesAsync()
        {
            return await GetRepositoriesAsync(new GettingRepositoriesFilter());
        }

        public async Task<IEnumerable<StockBookResultDto>> GetStockBookAsync(GettingStockBookFilter filter)
        {
            var receivedStocks = await _repoRepository.GetRepositoriesWithTheirStocksAsync(filter.ConvertToRepositoryFilter());

            return receivedStocks.SelectMany(r => r.ConvertToStockBookResultDtos());
        }

        public async Task<IEnumerable<StockBookResultDto>> GetStockBookAsync()
        {
            return await GetStockBookAsync(new GettingStockBookFilter());
        }

        public Task<BookReductionResultDto> ReduceBookAsync(BookReductionDto aBook)
        {
            throw new NotImplementedException();
        }

        public async Task<RemovalRespositoryResultDto> ChangeRepositoryActivation(RemovalRespositoryDto removalRepository)
        {
            var theRepository = _repoRepository.ChangeRepositoryActivation(removalRepository.Id, removalRepository.IsEnable);

            await _repoRepository.SaveChangesAsync();

            return theRepository.ConvertToRemovalRespositoryResultDto();
        }

        public async Task<StockingBookResultDto> StockBookAsync(StockingBookDto stockingBookDto)
        {
            var theRepository = await _repoRepository.FindAsync(stockingBookDto.RepositoryId);

            theRepository.Stocks.Add(new Stock
            {
                BookId = stockingBookDto.BookId,
                RepositoryId = theRepository.Id,
                Status = StockStatus.New,
            });

            await _repoRepository.SaveChangesAsync();

            var addedStock = theRepository.Stocks.FirstOrDefault();

            return new StockingBookResultDto
            {
                BookId = addedStock.BookId,
                StockId = addedStock.StockId,
            };
        }
    }
}
