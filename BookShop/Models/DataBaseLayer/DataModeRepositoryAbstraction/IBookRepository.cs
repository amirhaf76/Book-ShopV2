using BookShop.Models.DataBaseLayer.DataModelRepository;
using BookShop.Models.Dtos.BookDtos;

namespace BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction
{
    public interface IBookRepository
    {
        Task<BookCreationDto> CreateBookAsync(BookCreationDto createBookRequest);
        Task<IEnumerable<BookQueryDto>> GetAllBooksAsync(PaginationFilter paginationFilter);
    }
}