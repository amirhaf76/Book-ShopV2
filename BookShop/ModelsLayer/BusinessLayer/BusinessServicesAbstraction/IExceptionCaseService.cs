using BookShop.ModelsLayer.Exceptions;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction
{
    public interface IExceptionCaseService
    {
        UserAccountIsExistException UserAccountIsExistException(string username);
        UserAccountNotFoundException UserAccountNotFoundException(string username);
        UsernameOrPasswordIsIncorrectException UsernameOrPasswordIsIncorrectException();
    }
}
