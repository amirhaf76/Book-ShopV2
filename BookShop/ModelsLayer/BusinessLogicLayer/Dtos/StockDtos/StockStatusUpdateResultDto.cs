using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos
{
    public class StockStatusUpdateResultDto
    {
        public long StockId { get; set; }

        public StockStatusVariety Status { get; set; }
    }
}
