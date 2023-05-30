using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataAccessLayer.Dtos
{
    public class StockFilter
    {
        public StockFilter()
        {
            Pagination = new PaginationFilter(10, 1);
            StockIds = new List<long>();
            RepositoryIds = new List<int>();
            BookIds = new List<int>();
            ReservationIds = new List<int>();
            Statuses = new List<StockStatus>();
        }

        public PaginationFilter Pagination { get; set; }

        public IEnumerable<long> StockIds { get; set; }

        public IEnumerable<int> RepositoryIds { get; set; }

        public IEnumerable<int> BookIds { get; set; }

        public IEnumerable<int> ReservationIds { get; set; }

        public IEnumerable<StockStatus> Statuses { get; set; }
    }
}
