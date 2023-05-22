using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IUserPermissionRepository : IBaseRepository<UserPermission>
    {
        Task<IEnumerable<UserPermission>> GetUserPermissionByIdAsync(int userId);
    }
}
