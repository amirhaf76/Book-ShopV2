using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Exceptions;
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
            var stocks = await _stockRepository.GetStocksCompletelyAsync(filter.ConvertToStockFilter());

            return stocks.Select(x => x.ConvertToStockBookResultDto());
        }

        public async Task<IEnumerable<StockBookResultDto>> GetStockAsync()
        {
            return await GetStockAsync(new GettingStockBookFilter());
        }


        public async Task<StockingBookResultDto> StockBookAsync(StockingBookDto stockingBookDto)
        {
            var stock = stockingBookDto.ConvertToStock();

            stock.Status = StockStatus.New;

            stock = await _stockRepository.AddAsync(stock);

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

            return theStock.ConvertToUpdatingStockedBookResultDto();
        }

        public async Task<StockReservationResultDto> ReserveStockWithCheckingAsync(StockReservationDto stockReservation)
        {
            var theStocks = await _stockRepository.GetStocksCompletelyAsync(new DataAccessLayer.Dtos.StockFilter { StockIds = stockReservation.StockIds });

            foreach (var stock in theStocks)
            {
                stock.ReservationId = stockReservation.ReservationId;
            }

            await _stockRepository.SaveChangesAsync();

            return new StockReservationResultDto
            {
                StockId = theStocks.Where(s => s.ReservationId == stockReservation.ReservationId).Select(s => s.StockId),
                ReservationId = stockReservation.ReservationId,
            };
        }

        public async Task<StockStatusUpdateResultDto> UpdateStockStatus(StockStatusUpdateDto stockStatusUpdate)
        {
            var theStock = await _stockRepository.FindAsync(stockStatusUpdate.StockId);

            if (theStock == null)
            {
                throw new StockedBookNotFoundException();
            }

            theStock.Status = stockStatusUpdate.Status.ConvertToStockStatus();

            await _stockRepository.SaveChangesAsync();

            return theStock.ConvertToStockStatusUpdateResultDto();
        }
    }
}
