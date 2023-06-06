namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos
{
    public class BookReservationResultDto
    {
        public IEnumerable<long> StockId { get; set; }

        public long? ReservationId { get; set; }
    }
}