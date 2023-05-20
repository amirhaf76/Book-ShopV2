using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.Dtos.FilterDtos;
using BookShop.ModelsLayer.DtosExtension;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository, IScope
    {
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(ILogger<BookRepository> logger, DbContext dbContext) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<Book> GetAndLoadBookAsync(int id)
        {
            return await FindAndLoadPropertiesAsync(b => b.Authors, id);
        }


        public async Task<IEnumerable<BookQueryDto>> GetAllBooksAsync(PaginationFilter paginationFilter, BookFilterDto bookFilterDto)
        {//??????
            var queryable = _dbSet.AsQueryable();

            if (bookFilterDto.Id != null)
            {
                queryable = queryable.Where(q => q.Id == bookFilterDto.Id);
            }

            if (bookFilterDto.Title != null)
            {
                queryable = queryable.Where(q => EF.Functions.Like(q.Title,$"%{bookFilterDto.Title}%"));
            }

            if (bookFilterDto.PublishedYear != null)
            {
                var year = (int)bookFilterDto.PublishedYear?.Year;

                queryable = queryable.Where(q => q.PublishedDate != null && q.PublishedDate.Value.Year == year);
            }

            var receivedBooks = await queryable
                .OrderByDescending(x => x.Id)
                .Skip(paginationFilter.GetSkipNumber())
                .Take(paginationFilter.PageSize)
                .Include(x => x.Authors)
                .AsSplitQuery()
                .ToListAsync();

            return receivedBooks.Select(book => book.ConvertToBookQueryDto());
        }
    }
}
