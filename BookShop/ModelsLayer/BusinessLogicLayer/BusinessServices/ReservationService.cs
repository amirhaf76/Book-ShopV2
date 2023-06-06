using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class ReservationService : IReservationService, IScope
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IStockService _stockService;

        private readonly ILogger<ReservationService> _logger;


        public ReservationService(ILogger<ReservationService> logger, IReservationRepository reservationRepository, IStockService stockService)
        {
            _logger = logger;
            _reservationRepository = reservationRepository;
            _stockService = stockService;
        }

        public async Task<ReservedBookCancellationResultDto> CancelBookReservationAsync(ReservedBookCancellationDto reservationCancellation)
        {
            var theReservation = await _reservationRepository.GetReservationAsync(reservationCancellation.ReservationId);

            var stockCancellationResult = await _stockService.CancelStocksAsync(theReservation.Stocks.Select(s => s.StockId));

            return new ReservedBookCancellationResultDto
            {
                ReservationId = theReservation.Id,
                CanceledStocks = stockCancellationResult.CanceledStocks,
            };
        }

        public async Task<IEnumerable<BookReservationReportDto>> GetReservedBookAsync(ReservedBookFilterDto filter)
        {
            var reservedBooks = await _reservationRepository.GetReservationsAsync(filter.ConvertToReservationFilter());

            var stocks = await _stockService.GetStockAsync(new GettingStockBookFilter { ReservationIds = reservedBooks.Select(x => (int?)x.Id) });

            var result = reservedBooks.Join(
                stocks.GroupBy(x => x.ReservationId),
                reservedBook => reservedBook.Id,
                stock => stock.Key,
                (reservation, stock) => new BookReservationReportDto
                {
                    Id = reservation.Id,
                    ComfirmationTime = reservation.ComfirmationTime,
                    LastChange = reservation.LastChange,
                    UserAccountId = reservation.UserAccountId,
                    Stocks = stock
                        .Select(s => new ReservedStockResultDto
                        {
                            StockId = s.StockId,
                            BookId = s.Book.Id,
                            RepositoryId = s.Repository.Id,
                            ReservationId = s.ReservationId,
                        }),
                    Status = reservation.Status.ConvertToReservationStatusVariety(),
                });

            return result;
        }

        public async Task<BookReservationResultDto> ReserveBookAsync(BookReservationDto reservation)
        {
            var newReservation = await _reservationRepository.AddAsync(new Reservation
            {
                LastChange = DateTime.UtcNow,
                Status = ReservationStatus.Pending,
                UserAccountId = reservation.UserAccountId,
            });

            await _reservationRepository.SaveChangesAsync();

            var reservedBook = await _stockService.ReserveStockAsync(reservation.BookIds, newReservation.Id);

            return new BookReservationResultDto
            {
                StockId = reservedBook.StockId,
                ReservationId = reservedBook.ReservationId,
            };
        }
    }
}
