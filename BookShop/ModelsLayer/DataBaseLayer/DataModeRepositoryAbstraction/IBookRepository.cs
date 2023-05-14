using BookShop.ModelsLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.Dtos.FilterDtos;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModeRepositoryAbstraction
{
    public interface IBookRepository
    {
        Task<BookCreationDto> CreateBookAsync(BookCreationDto createBookRequest);
        Task<IEnumerable<BookQueryDto>> GetAllBooksAsync(PaginationFilter paginationFilter);
    }
}