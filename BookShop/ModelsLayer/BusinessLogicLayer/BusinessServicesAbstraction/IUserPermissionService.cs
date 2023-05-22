using System.Security.Claims;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IUserPermissionService
    {
        Task<IEnumerable<Claim>> GetUserClaimsAsync(int userId);
    }
}