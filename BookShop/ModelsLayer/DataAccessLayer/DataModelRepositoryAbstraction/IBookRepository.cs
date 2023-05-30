using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.Dtos.FilterDtos;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<BookQueryDto>> GetAllBooksAsync(PaginationFilter paginationFilter, BookFilterDto bookFilterDto);

        Task<Book> GetBookWithItsAuthorsAsync(int id);
    }
}