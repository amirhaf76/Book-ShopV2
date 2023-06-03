using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos
{
    public class ReservedBookFilterDto
    {
        public int? Id { get; set; }

        public PaginationFilterDto Pagination { get; set; }

        public TimeFilter LastChangeTime { get; set; }

        public TimeFilter ComfirmationTime { get; set; }

        public ReservationStatusVariety? Status { get; set; }

        public int? UserAccountId { get; set; }
    }
}
