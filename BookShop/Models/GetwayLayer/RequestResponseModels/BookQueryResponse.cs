using BookShop.Models.Dtos;

namespace BookShop.Models.GetwayLayer.RequestResponseModels
{
    public class BookQueryResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int PageNumbers { get; set; }

        public IEnumerable<AuthorDto> Authors { get; set; } = Array.Empty<AuthorDto>();
    }
}
