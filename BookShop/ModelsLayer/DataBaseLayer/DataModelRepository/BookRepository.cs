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
        private readonly DbContext _dbContext;

        public BookRepository(ILogger<BookRepository> logger, DbContext dbContext) : base(dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<BookQueryDto>> GetAllBooksAsync(PaginationFilter paginationFilter)
        {
            var receivedBooks = await _dbContext
                .Set<Book>()
                .OrderByDescending(x => x.Id)
                .Include(x => x.Authors)
                .Skip(paginationFilter.GetSkipNumber())
                .Take(paginationFilter.PageSize)
                .ToListAsync();

            return receivedBooks.Select(BookDtosExtension.ConvertToBookCreationDto);
        }
    }
}
