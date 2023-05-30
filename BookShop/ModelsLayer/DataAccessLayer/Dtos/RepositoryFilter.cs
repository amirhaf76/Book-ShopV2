using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.Dtos.FilterDtos;

namespace BookShop.ModelsLayer.DataAccessLayer.Dtos
{
    public class RepositoryFilter
    {
        public RepositoryFilter()
        {
            Pagination = new PaginationFilter(10, 1);
        }

        public PaginationFilter Pagination { get; set; }

        public int? Id { get; set; }

        public bool? IsEnable { get; set; }

        public string Name { get; set; }

        public int? AddressId { get; set; }

    }

    public class ReservationFilter
    {
        public int Id { get; set; }

        public DateTime LastChange { get; set; }

        public DateTime? ComfirmationTime { get; set; }

        public ReservationStatus Status { get; set; }

        public int UserAccountId { get; set; }
    }
}
