using BookShop.Models.Exceptions;

namespace BookShop.Models.BusinessServiceAbstraction
{
    public interface IExceptionCaseService
    {
        UserAccountIsExistException UserAccountIsExistException(string username);
        UserAccountNotFoundException UserAccountNotFoundException(string username);
        UsernameOrPasswordIsIncorrectException UsernameOrPasswordIsIncorrectException();
    }
}
