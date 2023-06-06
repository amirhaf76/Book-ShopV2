namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.StockDtos
{
    public class ReservationCancellationResultDto
    {
        public ReservationCancellationResultDto()
        {
            CanceledStocks = new List<long>();
        }

        public IEnumerable<long> CanceledStocks;
    }
}
