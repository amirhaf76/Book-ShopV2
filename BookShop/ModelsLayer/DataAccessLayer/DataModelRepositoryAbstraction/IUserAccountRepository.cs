using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction
{
    public interface IUserAccountRepository : IBaseRepository<UserAccount>
    {
        Task<UserAccount> GetUserAccountAsync(string username);
    }
}