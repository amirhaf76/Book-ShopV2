namespace BookShop.Models.DataBaseLayer.DataBaseModels
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; } = DateTime.MinValue;

        public int Pages { get; set; }

        public ICollection<Author> Authors { get; set; }
    }
}
