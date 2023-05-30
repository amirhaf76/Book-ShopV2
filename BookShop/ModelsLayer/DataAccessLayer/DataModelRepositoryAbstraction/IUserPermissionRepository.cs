using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface IUserPermissionRepository : IBaseRepository<UserPermission>
    {
        Task<IEnumerable<UserPermission>> GetUserPermissionByIdAsync(int userId);
    }
}
