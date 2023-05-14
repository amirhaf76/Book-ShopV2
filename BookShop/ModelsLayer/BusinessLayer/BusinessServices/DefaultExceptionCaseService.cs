using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.Exceptions;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServices
{
    public class DefaultExceptionCaseService : IExceptionCaseService, IScope
    {
        public UserAccountIsExistException UserAccountIsExistException(string username)
        {
            return new UserAccountIsExistException($"Username '{username}' is exist in database!");
        }

        public UserAccountNotFoundException UserAccountNotFoundException(string username)
        {
            return new UserAccountNotFoundException($"Username '{username}' is exist in database!");
        }

        public UsernameOrPasswordIsIncorrectException UsernameOrPasswordIsIncorrectException()
        {
            return new UsernameOrPasswordIsIncorrectException($"The username or password is incorrect!");
        }
    }
}
