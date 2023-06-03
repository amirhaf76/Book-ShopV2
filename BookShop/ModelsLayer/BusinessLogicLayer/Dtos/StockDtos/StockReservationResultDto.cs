namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos
{
    public class StockReservationResultDto
    {
        public IEnumerable<long> StockId { get; set; }

        public int? ReservationId { get; set; }
    }
}
