namespace BookShop.ModelsLayer.DataBaseLayer.DataBaseModels
{
    public class Book
    {
        public Book()
        {
            Authors = new List<Author>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedDate { get; set; }

        public int Pages { get; set; }

        public ICollection<Author> Authors { get; set; }
    }
}
