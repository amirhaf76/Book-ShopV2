using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<Author> FindAuthorAsync(int id);

        Task<Author> FindAuthorOrDefaultAsync(int id);

        Task<IEnumerable<Author>> FindAuthorsByIdAsync(IEnumerable<int> authorIds);
    }
}