namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos
{
    public class StockedBookResult
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int PageNumbers { get; set; }

        public DateTime? PublishedDate { get; set; }
    }
}
