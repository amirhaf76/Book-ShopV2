namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos
{
    public class BookReservationReportDto
    {
        public BookReservationReportDto()
        {
            Stocks = new List<ReservedStockResultDto>();
        }

        public int Id { get; set; }

        public DateTime LastChange { get; set; }

        public DateTime? ComfirmationTime { get; set; }

        public ReservationStatusVariety Status { get; set; }

        public int UserAccountId { get; set; }

        public IEnumerable<ReservedStockResultDto> Stocks { get; set; }

    }

}