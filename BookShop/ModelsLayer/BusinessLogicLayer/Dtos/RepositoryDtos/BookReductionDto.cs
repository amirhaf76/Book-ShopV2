namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class BookReductionDto
    {
        public BookReductionDto()
        {
            StockIds = new List<long>();
        }

        public IEnumerable<long> StockIds { get; set; }
    }
}
