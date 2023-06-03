namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos
{
    public class ReservedStockResultDto
    {
        public long StockId { get; set; }

        public int RepositoryId { get; set; }
        public int BookId { get; set; }
        public int? ReservationId { get; set; }

    }

}