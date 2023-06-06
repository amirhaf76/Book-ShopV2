using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension
{
    public static class StockDtoExtension
    {
        public static Stock UpdateStock(this Stock stock, UpdatingStockedBookDto updatingStock)
        {
            if (updatingStock.Status != null)
            {
                stock.Status = ((StockStatusVariety)updatingStock.Status).ConvertToStockStatus();
            }

            if (updatingStock.BookId != null)
            {
                stock.BookId = (int)updatingStock.BookId;
            }

            if (updatingStock.ReservationId != null)
            {
                stock.ReservationId = (int)updatingStock.ReservationId;
            }

            if (updatingStock.RepositoryId != null)
            {
                stock.RepositoryId = (int)updatingStock.RepositoryId;
            }

            return stock;
        }

        public static UpdatingStockedBookResultDto ConvertToUpdatingStockedBookResultDto(this Stock stock)
        {
            return new UpdatingStockedBookResultDto
            {
                StockId = stock.StockId,
                BookId = stock.BookId,
                RepositoryId = stock.RepositoryId,
                ReservationId = stock.ReservationId,
                Status = stock.Status.ConvertToStockStatusVariety(),
            };
        }

        public static Stock ConvertToStock(this StockingBookDto stockingBook)
        {
            return new Stock
            {
                RepositoryId = stockingBook.RepositoryId,
                BookId = stockingBook.BookId,
                ReservationId = stockingBook.ReservationId,
                Status = StockStatus.New,
            };
        }

        public static StockingBookResultDto ConvertToStockingBookResultDto(this Stock stock)
        {
            return new StockingBookResultDto
            {
                StockId = stock.StockId,
                BookId = stock.BookId,
                RepositoryId = stock.RepositoryId,
                ReservationId = stock.ReservationId,
                Status = stock.Status.ConvertToStockStatusVariety(),
            };
        }

        public static StockFilter ConvertToStockFilter(this GettingStockBookFilter filter)
        {
            return new StockFilter
            {
                BookIds = filter.BookIds,
                RepositoryIds = filter.RepositoryIds,
                ReservationIds = filter.ReservationIds,
                StockIds = filter.StockIds,
                Statuses = filter.Statuses.Select(s => s.ConvertToStockStatus()),
                Pagination = filter.Pagination?.ConvertToPaginationFilter() ?? new PaginationFilter(10, 1),
            };
        }

        public static StockBookResultDto ConvertToStockBookResultDto(this Stock stock)
        {
            return new StockBookResultDto
            {
                StockId = stock.StockId,
                ReservationId = stock.ReservationId,
                Book = stock.Book.ConvertToStockedBookResult(),
                Repository = stock.Repository.ConvertToRespositoryMinResult(),
                Status = stock.Status.ConvertToStockStatusVariety(),
            };
        }

        public static StockStatusUpdateResultDto ConvertToStockStatusUpdateResultDto(this Stock stock)
        {
            return new StockStatusUpdateResultDto
            {
                StockId = stock.StockId,
                Status = stock.Status.ConvertToStockStatusVariety(),
            };
        }

    }
}
