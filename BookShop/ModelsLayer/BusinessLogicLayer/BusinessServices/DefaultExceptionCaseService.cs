using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.Exceptions;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class DefaultExceptionCaseService : IExceptionCaseService, IScope
    {
        public UserAccountIsExistException CreateUserAccountIsExistException(string username)
        {
            return new UserAccountIsExistException($"Username '{username}' is exist in database!");
        }

        public UserAccountNotFoundException CreateUserAccountNotFoundException(string username)
        {
            return new UserAccountNotFoundException($"Username '{username}' is exist in database!");
        }

        public UsernameOrPasswordIsIncorrectException CreateUsernameOrPasswordIsIncorrectException()
        {
            return new UsernameOrPasswordIsIncorrectException($"The username or password is incorrect!");
        }

        public BookNotFoundException CreateBookNotFoundException(int id)
        {
            return new BookNotFoundException($"There is no book with '{id}' id.");
        }

        public AuthorNotFoundException CreateAuthorNotFoundException(int id)
        {
            return new AuthorNotFoundException($"There is no author with '{id}' id.");
        }

    }
}
