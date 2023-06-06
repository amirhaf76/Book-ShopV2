namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos
{
    public class BookReservationDto
    {
        public IEnumerable<int> BookIds { get; set; }

        public int UserAccountId { get; set; }
    }
}