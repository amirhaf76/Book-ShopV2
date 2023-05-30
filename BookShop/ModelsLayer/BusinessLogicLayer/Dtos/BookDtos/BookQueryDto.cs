using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.AuthorDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos
{
    public class BookQueryDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int PageNumbers { get; set; }

        public DateTime? PublishedDate { get; set; }

        public IEnumerable<AuthorDto> Authors { get; set; } = Array.Empty<AuthorDto>();
    }
}
