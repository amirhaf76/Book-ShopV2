namespace BookShop.ModelsLayer.Dtos.BookDtos
{
    public class BookFilterDto
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedYear { get; set; }
    }
}
