namespace BookShop.Models.GetwayLayer.RequestResponseModels
{
    public class BookCreationRequest
    {
        public string Title { get; set; } = string.Empty;

        public int PageNumbers { get; set; }

        public IEnumerable<int> AuthorIds { get; set; } = Array.Empty<int>();
    }
}
