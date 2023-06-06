using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.BusinessLogicLayer.SemaphorModels;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
using BookShop.ModelsLayer.Exceptions;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class StockService : IStockService, IScope
    {
        private static int s_readerCounter;

        private readonly IStockRepository _stockRepository;

        private readonly Semaphore _readerSemaphore;
        private readonly Semaphore _writerSemaphore;

        static StockService()
        {
            s_readerCounter = 0;
        }

        public StockService(IStockRepository stockRepository, StockServiceReaderSemaphore readerSemaphore, StockServiceWriterSemaphore writerSemaphore)
        {
            _stockRepository = stockRepository;

            _readerSemaphore = readerSemaphore.Semaphore;
            _writerSemaphore = writerSemaphore.Semaphore;
        }


        public async Task<IEnumerable<StockBookResultDto>> GetStockAsync(GettingStockBookFilter filter)
        {
            EnterAsReader();

            var stocks = await _stockRepository.GetStocksCompletelyAsync(filter.ConvertToStockFilter());

            ExitAsReader();

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

            _writerSemaphore.WaitOne();

            stock = await _stockRepository.AddAsync(stock);

            await _stockRepository.SaveChangesAsync();

            _writerSemaphore.Release();

            return stock.ConvertToStockingBookResultDto();
        }

        public async Task<UpdatingStockedBookResultDto> UpdateStockAsync(UpdatingStockedBookDto updatingStock)
        {
            _writerSemaphore.WaitOne();

            var theStock = await _stockRepository.FindAsync(updatingStock.StockId);

            if (theStock == null)
            {
                throw new StockedBookNotFoundException();
            }

            theStock = theStock.UpdateStock(updatingStock);


            await _stockRepository.SaveChangesAsync();

            _writerSemaphore.Release();

            return theStock.ConvertToUpdatingStockedBookResultDto();
        }

        public async Task<StockReservationResultDto> ReserveStockAsync(StockReservationDto stockReservation)
        {
            _writerSemaphore.WaitOne();

            var theStocks = await _stockRepository.GetStocksCompletelyAsync(new DataAccessLayer.Dtos.StockFilter { StockIds = stockReservation.StockIds });

            foreach (var stock in theStocks)
            {
                stock.ReservationId = stockReservation.ReservationId;
            }

            await _stockRepository.SaveChangesAsync();

            _writerSemaphore.Release();

            return new StockReservationResultDto
            {
                StockId = theStocks.Where(s => s.ReservationId == stockReservation.ReservationId).Select(s => s.StockId),
                ReservationId = stockReservation.ReservationId,
            };
        }

        public async Task<StockReservationResultDto> ReserveStockAsync(IEnumerable<int> bookIds, int reservationId)
        {
            _writerSemaphore.WaitOne();

            var theStocks = await _stockRepository.GetStocksCompletelyAsync(new DataAccessLayer.Dtos.StockFilter { BookIds = bookIds });

            var groupedStocks = theStocks.GroupBy(x => x.BookId);

            if (bookIds.ExceptBy(groupedStocks.Select(x => x.Key), x => x).Any())
            {
                throw new UnavailableStockException();
            }

            foreach (var group in groupedStocks)
            {
                group.FirstOrDefault().ReservationId = reservationId;
            }

            await _stockRepository.SaveChangesAsync();

            _writerSemaphore.Release();

            return new StockReservationResultDto
            {
                StockId = theStocks.Where(s => s.ReservationId == reservationId).Select(s => s.StockId),
                ReservationId = reservationId,
            };
        }

        public async Task<ReservationCancellationResultDto> CancelStocksAsync(IEnumerable<long> stockIds)
        {
            _writerSemaphore.WaitOne();

            var theStocks = await _stockRepository.GetStocksAsync(new StockFilter { StockIds = stockIds });

            if (theStocks.Count() != stockIds.Count())
            {
                throw new CancellingUnexistedStockException();
            }

            foreach (var stock in theStocks)
            {
                stock.ReservationId = null;
            }

            await _stockRepository.SaveChangesAsync();

            _writerSemaphore.Release();

            return new ReservationCancellationResultDto
            {
                CanceledStocks = theStocks.Select(x => x.StockId),
            };
        }

        public async Task<StockStatusUpdateResultDto> UpdateStockStatus(StockStatusUpdateDto stockStatusUpdate)
        {
            _writerSemaphore.WaitOne();

            var theStock = await _stockRepository.FindAsync(stockStatusUpdate.StockId);

            if (theStock == null)
            {
                throw new StockedBookNotFoundException();
            }

            theStock.Status = stockStatusUpdate.Status.ConvertToStockStatus();


            await _stockRepository.SaveChangesAsync();

            _writerSemaphore.Release();

            return theStock.ConvertToStockStatusUpdateResultDto();
        }

        private void EnterAsReader()
        {
            _readerSemaphore.WaitOne();

            s_readerCounter++;

            if (s_readerCounter == 1)
            {
                _writerSemaphore.WaitOne();
            }

            _readerSemaphore.Release();
        }

        private void ExitAsReader()
        {
            _readerSemaphore.WaitOne();

            s_readerCounter--;

            if (s_readerCounter == 0)
            {
                _writerSemaphore.Release();
            }

            _readerSemaphore.Release();
        }
    }
}
