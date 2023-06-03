using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;

namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class GettingStockBookFilter
    {

        public GettingStockBookFilter()
        {
            StockIds = new List<long>();
            RepositoryIds = new List<int>();
            BookIds = new List<int>();
            ReservationIds = new List<int?>();
            Statuses = new List<StockStatusVariety>();
        }

        public PaginationFilterDto Pagination { get; set; }

        public IEnumerable<long> StockIds { get; set; }

        public IEnumerable<int> RepositoryIds { get; set; }

        public IEnumerable<int> BookIds { get; set; }

        public IEnumerable<int?> ReservationIds { get; set; }

        public IEnumerable<StockStatusVariety> Statuses { get; set; }
    }
}
