using BookShop.Models.BusinessServiceAbstraction;
using BookShop.Models.Exceptions;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.Models.BusinessServices
{
    public class DefaultExceptionCaseService : IExceptionCaseService, IScope
    {
        public UserAccountIsExistException UserAccountIsExistException(string username)
        {
            return new UserAccountIsExistException($"Username '{username}' is exist in database!");
        }
    }
}
