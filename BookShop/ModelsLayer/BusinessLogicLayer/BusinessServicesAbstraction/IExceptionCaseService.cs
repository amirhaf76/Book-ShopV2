﻿using BookShop.ModelsLayer.Exceptions;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IExceptionCaseService
    {
        UserAccountIsExistException CreateUserAccountIsExistException(string username);

        UserAccountNotFoundException CreateUserAccountNotFoundException(string username);

        UsernameOrPasswordIsIncorrectException CreateUsernameOrPasswordIsIncorrectException();

        BookNotFoundException CreateBookNotFoundException(int id);

        AuthorNotFoundException CreateAuthorNotFoundException(int id);
    }
}
