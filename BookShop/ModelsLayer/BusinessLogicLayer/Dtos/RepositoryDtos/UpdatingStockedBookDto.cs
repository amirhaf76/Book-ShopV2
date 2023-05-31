namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class UpdatingStockedBookDto
    {
        public long StockId { get; set; }

        public int? BookId { get; set; }

        public int? RepositoryId { get; set; }

        public int? ReservationId { get; set; }

        public StockStatusVariety? Status { get; set; }

    }

}
