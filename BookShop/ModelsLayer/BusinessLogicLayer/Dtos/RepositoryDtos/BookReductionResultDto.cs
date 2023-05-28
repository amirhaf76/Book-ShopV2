namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class BookReductionResultDto
    {
        public BookReductionResultDto()
        {
            ReducedQuantity = new List<long>();
        }
        public IEnumerable<long> ReducedQuantity { get; set; }
    }
}
