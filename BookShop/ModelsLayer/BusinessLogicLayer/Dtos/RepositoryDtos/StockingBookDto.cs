namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class StockingBookDto
    {
        public int RepositoryId { get; set; }

        public int BookId { get; set; }

        public int? ReservationId { get; set; }
    }

}
