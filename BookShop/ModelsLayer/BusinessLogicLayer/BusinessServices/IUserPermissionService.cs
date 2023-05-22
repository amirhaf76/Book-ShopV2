using System.Security.Claims;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public interface IUserPermissionService
    {
        Task<IEnumerable<Claim>> GetUserClaimsAsync(int userId);
    }
}