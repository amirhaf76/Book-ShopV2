using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IUserAccountRepository : IBaseRepository<UserAccount>
    {
        Task<UserAccount> GetUserAccountAsync(string username);
    }
}