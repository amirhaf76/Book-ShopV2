namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos
{
    public class StockReservationResultDto
    {
        public StockReservationResultDto()
        {
            StockId = new List<long>();
        }

        public IEnumerable<long> StockId { get; set; }

        public int? ReservationId { get; set; }
    }
}
