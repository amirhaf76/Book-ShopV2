namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class StockBooksResultDto
    {
        public StockBooksResultDto()
        {
            StockBooks = new List<StockBookResultDto>();
        }

        public IEnumerable<StockBookResultDto> StockBooks { get; set; }
    }
}
