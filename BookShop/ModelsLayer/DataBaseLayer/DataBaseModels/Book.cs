namespace BookShop.ModelsLayer.DataBaseLayer.DataBaseModels
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedDate { get; set; }

        public int Pages { get; set; }

        public ICollection<Author> Authors { get; set; }
    }
}
