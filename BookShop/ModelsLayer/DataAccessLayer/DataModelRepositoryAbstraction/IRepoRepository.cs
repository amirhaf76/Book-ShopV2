using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IRepoRepository : IBaseRepository<Repository>
    {
        Task<IEnumerable<Repository>> GetRepositoriesAsync();

        Task<IEnumerable<Repository>> GetRepositoriesAsync(RepositoryFilter filter);

        Task<IEnumerable<Repository>> GetRepositoriesWithTheirStocksAsync();

        Task<IEnumerable<Repository>> GetRepositoriesWithTheirStocksAsync(RepositoryFilter filter);
    }


}
