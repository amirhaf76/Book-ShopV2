using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {

        Task<Author> FindAuthorAsync(Author author);
        Task<Author> FindAuthorAsync(int id);
        Task<Author> FindAuthorByIdOrDefaultAsync(int id);
        Task<Author> FindAuthorOrDefaultAsync(Author author);
        Task<IEnumerable<Author>> FindAuthorsByIdAsync(IEnumerable<int> authorIds);
    }
}