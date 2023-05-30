namespace BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.RequestResponseModels
{
    public class GetAllBooksRequest
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int? Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedYear { get; set; }
    }
}
