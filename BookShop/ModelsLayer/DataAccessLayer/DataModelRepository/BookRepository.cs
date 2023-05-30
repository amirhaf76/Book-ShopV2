﻿using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.FilterDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository, IScope
    {
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(ILogger<BookRepository> logger, DbContext dbContext) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<Book> GetBookWithItsAuthorsAsync(int id)
        {
            var theBook = await FindAsync(id);

            if (theBook == null)
            {
                return null;
            }

            var bookEntry = _dbSet.Entry(theBook);

            await bookEntry.Collection(b => b.Authors).LoadAsync();

            return theBook;
        }


        public async Task<IEnumerable<BookQueryDto>> GetAllBooksAsync(PaginationFilter paginationFilter, BookFilterDto bookFilterDto)
        {
            var queryable = _dbSet.AsQueryable();

            if (bookFilterDto.Id != null)
            {
                queryable = queryable.Where(q => q.Id == bookFilterDto.Id);
            }

            if (bookFilterDto.Title != null)
            {
                queryable = queryable.Where(q => EF.Functions.Like(q.Title, $"%{bookFilterDto.Title}%"));
            }

            if (bookFilterDto.PublishedYear != null)
            {
                var year = (int)bookFilterDto.PublishedYear?.Year;

                queryable = queryable.Where(q => q.PublishedDate != null && q.PublishedDate.Value.Year == year);
            }

            var receivedBooks = await queryable
                .OrderByDescending(x => x.Id)
                .Skip(paginationFilter.GetSkipSize())
                .Take(paginationFilter.PageSize)
                .Include(x => x.Authors)
                .AsSplitQuery()
                .ToListAsync();

            return receivedBooks.Select(book => book.ConvertToBookQueryDto());
        }
    }
}
