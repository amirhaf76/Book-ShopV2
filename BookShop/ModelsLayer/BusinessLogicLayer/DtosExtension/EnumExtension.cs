using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using Infrastructure.Tools;

namespace BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension
{
    public static class EnumExtension
    {
        public static StockStatusVariety ConvertToStockStatusVariety(this StockStatus stockStatus)
        {
            return stockStatus switch
            {
                StockStatus.New => StockStatusVariety.New,
                StockStatus.Sold => StockStatusVariety.Sold,
                StockStatus.ReTurned => StockStatusVariety.ReTurned,
                StockStatus.Dropped => StockStatusVariety.Dropped,

                _ => throw Helper.CreateExceptionForInvaidEnumValue(stockStatus),
            };
        }

        public static StockStatus ConvertToStockStatus(this StockStatusVariety stockStatus)
        {
            return stockStatus switch
            {
                StockStatusVariety.New => StockStatus.New,
                StockStatusVariety.Sold => StockStatus.Sold,
                StockStatusVariety.ReTurned => StockStatus.ReTurned,
                StockStatusVariety.Dropped => StockStatus.Dropped,

                _ => throw Helper.CreateExceptionForInvaidEnumValue(stockStatus),
            };
        }
    }
}
