using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.FilterDtos;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<IEnumerable<BookQueryDto>> GetAllBooksAsync(PaginationFilter paginationFilter, BookFilterDto bookFilterDto);

        Task<Book> GetBookWithItsAuthorsAsync(int id);
    }
}