using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<Author> FindAuthorAsync(int id);

        Task<Author> FindAuthorOrDefaultAsync(int id);

        Task<IEnumerable<Author>> FindAuthorsByIdAsync(IEnumerable<int> authorIds);
    }
}