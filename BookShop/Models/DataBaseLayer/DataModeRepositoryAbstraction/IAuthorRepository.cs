﻿using BookShop.Models.DataBaseLayer.DataBaseModels;

namespace BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction
{
    public interface IAuthorRepository
    {
        Task<Author> AddAuthorIfItDoesntExistAsync(Author author);
        Task<Author> FindAuthorAsync(Author author);
        Task<Author> FindAuthorByIdAsync(int id);
        Task<Author> FindAuthorByIdOrDefaultAsync(int id);
        Task<Author> FindAuthorOrDefaultAsync(Author author);
        Task<IEnumerable<Author>> FindAuthorsByIdAsync(IEnumerable<int> authorIds);
    }
}