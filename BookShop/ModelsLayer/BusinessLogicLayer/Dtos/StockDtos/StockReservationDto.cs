namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos
{
    public class StockReservationDto
    {
        public IEnumerable<long> StockIds { get; set; }

        public int? ReservationId { get; set; }
    }
}
