using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.Core.Security.Authorization
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionPolicyAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _permissions;

        public PermissionPolicyAttribute(string permissionLetters)
        {
            _permissions = permissionLetters;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims.Where(c => c.Type == PermissionFactory.ClaimKey);

            if (!claims.Any())
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var permissionParts = _permissions.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var claimValues = claims.Select(x => x.Value);

            foreach (var permission in permissionParts)
            {
                if (!claimValues.Contains(permission))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}
