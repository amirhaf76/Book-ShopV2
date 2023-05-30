using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataAccessLayer.Dtos
{
    public class ReservationFilter
    {
        public ReservationFilter()
        {
            Pagination = new PaginationFilter(10, 1);
        }

        public int? Id { get; set; }

        public PaginationFilter Pagination { get; set; }

        public TimeFilter LastChange { get; set; }

        public TimeFilter ComfirmationTime { get; set; }

        public ReservationStatus? Status { get; set; }

        public int? UserAccountId { get; set; }
    }

}
