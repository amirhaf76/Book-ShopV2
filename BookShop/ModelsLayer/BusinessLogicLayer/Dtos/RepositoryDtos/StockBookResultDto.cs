using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class StockBookResultDto
    {
        public long StockId { get; set; }

        public RespositoryMinResult Repository { get; set; }

        public StockedBookResult Book { get; set; }

        public int? ReservationId { get; set; }

        // public Reservation Reservation { get; set; }


        public StockStatusVariety Status { get; set; }
    }
}
