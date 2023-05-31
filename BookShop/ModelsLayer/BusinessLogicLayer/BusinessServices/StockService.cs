using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class StockService : IStockService, IScope
    {
        private readonly IStockRepository _stockRepository;


        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }


        public async Task<IEnumerable<StockBookResultDto>> GetStockAsync(GettingStockBookFilter filter)
        {
            var stocks = await _stockRepository.GetStocksCompletlyAsync(filter.ConvertToStockFilter());

            return stocks.Select(x => x.ConvertToStockBookResultDto());
        }

        public async Task<IEnumerable<StockBookResultDto>> GetStockAsync()
        {
            return await GetStockAsync(new GettingStockBookFilter());
        }

        public async Task<StockingBookResultDto> StockBookAsync(StockingBookDto stockingBookDto)
        {
            var stock = await _stockRepository.AddAsync(stockingBookDto.ConvertToStock());

            await _stockRepository.SaveChangesAsync();

            return stock.ConvertToStockingBookResultDto();
        }

        public async Task<UpdatingStockedBookResultDto> UpdateStockAsync(UpdatingStockedBookDto updatingStock)
        {
            var theStock = _stockRepository.Attach(new Stock
            {
                StockId = updatingStock.StockId,
            });

            theStock = theStock.UpdateStock(updatingStock);

            await _stockRepository.SaveChangesAsync();

            return theStock.ConvertToUpdatingStockedBooResultDto();
        }
    }
}
