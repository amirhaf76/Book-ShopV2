using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.StockVMs;
using Infrastructure.Tools;

namespace BookShop.ModelsLayer.ViewModelLayer.VMExtension
{
    public static class StockVMExtension
    {
        public static StockCreationResultVM ConvertToStockCreationResultVM(this StockingBookResultDto stock)
        {
            return new StockCreationResultVM
            {
                StockId = stock.StockId,
                BookId = stock.BookId,
                RepositoryId = stock.RepositoryId,
                ReservationId = stock.ReservationId,
                Status = stock.Status.ConvertToStockStatusVarietyVM(),

            };
        }

        public static StockingBookDto ConvertToStockingBookDto(this StockCreationVM stock)
        {
            return new StockingBookDto
            {
                BookId = stock.BookId,
                RepositoryId = stock.RepositoryId,
                ReservationId = stock.ReservationId,
            };
        }

        public static StockStatusVarietyVM ConvertToStockStatusVarietyVM(this StockStatusVariety stockStatus)
        {
            return stockStatus switch
            {
                StockStatusVariety.New => StockStatusVarietyVM.New,
                StockStatusVariety.Sold => StockStatusVarietyVM.Sold,
                StockStatusVariety.ReTurned => StockStatusVarietyVM.ReTurned,
                StockStatusVariety.Dropped => StockStatusVarietyVM.Dropped,

                _ => throw Helper.CreateExceptionForInvaidEnumValue(stockStatus),
            };
        }

        public static StockStatusVariety ConvertToStockStatusVariety(this StockStatusVarietyVM stockStatus)
        {
            return stockStatus switch
            {
                StockStatusVarietyVM.New => StockStatusVariety.New,
                StockStatusVarietyVM.Sold => StockStatusVariety.Sold,
                StockStatusVarietyVM.ReTurned => StockStatusVariety.ReTurned,
                StockStatusVarietyVM.Dropped => StockStatusVariety.Dropped,

                _ => throw Helper.CreateExceptionForInvaidEnumValue(stockStatus),
            };
        }
    }
}
