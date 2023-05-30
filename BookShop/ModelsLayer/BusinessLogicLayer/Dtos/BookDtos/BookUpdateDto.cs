namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos
{
    public class BookUpdateDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PageNumbers { get; set; }

        public DateTime? PublishedDate { get; set; }

        public IEnumerable<int> AuthorIds { get; set; } = Array.Empty<int>();
    }
}
