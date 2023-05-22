using BookShop.Core.Security.Authorization;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;
using System.Security.Claims;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class UserPermissionService : IScope, IUserPermissionService
    {
        private readonly ILogger<UserPermissionService> _logger;
        private readonly IUserPermissionRepository _userPermissionRepository;

        public UserPermissionService(ILogger<UserPermissionService> logger, IUserPermissionRepository userPermissionRepository)
        {
            _logger = logger;
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task<IEnumerable<Claim>> GetUserClaimsAsync(int userId)
        {
            var permissions = await _userPermissionRepository.GetUserPermissionByIdAsync(userId);

            return permissions.Select(x => new Claim(PermissionFactory.ClaimKey, x.Permission.Name));
        }

        private static string GetKey(UserPermission permission)
        {
            return GetPermissionObject(permission.Permission.Name);
        }

        private static string GetValue(UserPermission permission)
        {
            return GetValue(permission.Permission.Name);
        }

        private static string GetValue(string name)
        {
            var startIndex = name.LastIndexOf('_') + 1;

            return name[startIndex..];
        }

        private static string GetPermissionObject(string name)
        {
            return name[..name.IndexOf('_')];
        }
    }
}
