namespace BookShop.ModelsLayer.DataAccessLayer.DataBaseModels
{
    public class Book
    {
        public Book()
        {
            Authors = new List<Author>();
            Stocks = new List<Stock>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedDate { get; set; }

        public int Pages { get; set; }

        public ICollection<Author> Authors { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}
