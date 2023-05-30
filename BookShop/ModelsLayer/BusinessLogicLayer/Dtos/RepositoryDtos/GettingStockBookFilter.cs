using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;

namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class GettingStockBookFilter
    {
        public PaginationFilterDto Pagination { get; set; }

        public int? Id { get; set; }

        public bool? IsEnable { get; set; }

        public string Name { get; set; }

        public int? AddressId { get; set; }
    }
}
