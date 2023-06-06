namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos
{
    public class ReservedBookCancellationResultDto
    {
        public ReservedBookCancellationResultDto()
        {
            CanceledStocks = new List<long>();
        }

        public int ReservationId { get; set; }

        public IEnumerable<long> CanceledStocks;
    }
}