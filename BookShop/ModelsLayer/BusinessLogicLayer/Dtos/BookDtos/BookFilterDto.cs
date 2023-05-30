using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;

namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos
{
    public class BookFilterDto
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedYear { get; set; }

        public PaginationFilterDto PaginationFilterDto { get; set; }
    }
}
