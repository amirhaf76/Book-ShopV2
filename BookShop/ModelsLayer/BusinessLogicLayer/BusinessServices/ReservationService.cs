using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class ReservationService : IReservationService, IScope
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IStockService _stockService;
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<ReservationService> _logger;

        private readonly ReservationServiceLock _serviceLock;

        public ReservationService(ILogger<ReservationService> logger, IReservationRepository reservationRepository, IStockRepository stockRepository, IStockService stockService, ReservationServiceLock serviceLock)
        {
            _logger = logger;
            _reservationRepository = reservationRepository;
            _stockRepository = stockRepository;
            _stockService = stockService;
            _serviceLock = serviceLock;
        }

        public async Task<ReservedBookCancellationResultDto> CancelBookReservationAsync(ReservedBookCancellationDto reservation)
        {

            throw new NotImplementedException();

        }

        public async Task<IEnumerable<BookReservationReportDto>> GetReservedBookAsync(ReservedBookFilterDto filter)
        {
            var reservedBooks = await _reservationRepository.GetReservationWithTheirStocksAsync(filter.ConvertToReservationFilter());

            return reservedBooks.Select(x => new BookReservationReportDto
            {
                Id = x.Id,
                ComfirmationTime = x.ComfirmationTime,
                LastChange = x.LastChange,
                UserAccountId = x.UserAccountId,
                Stocks = x.Stocks
                    .Select(s => new ReservedStockResultDto
                    {
                        StockId = s.StockId,
                        BookId = s.BookId,
                        RepositoryId = s.RepositoryId,
                        ReservationId = s.ReservationId,
                    }),
                Status = x.Status.ConvertToReservationStatusVariety(),
            });
        }

        public Task<BookReservationResultDto> ReserveBookAsync(BookReservationDto aBook)
        {
            lock (_serviceLock)
            {

            }
        }
    }
}
