using BookShop.Models.DataBaseLayer.DataBaseModels;
using BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.Models.DataBaseLayer.DbContexts.BookShopDbContexts;
using BookShop.Models.Dtos.BookDtos;
using BookShop.Models.DtosExtension;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Models.DataBaseLayer.DataModelRepository
{
    public class BookRepository : IBookRepository, IScope
    {
        private readonly ILogger<BookRepository> _logger;
        private readonly IAuthorRepository _authorRepository;
        private readonly DbContext _dbContext;

        public BookRepository(ILogger<BookRepository> logger, IAuthorRepository authorRepository, IBookShopDbContext bookShopDbContext)
        {
            _logger = logger;
            _authorRepository = authorRepository;
            _dbContext = bookShopDbContext.GetDbContext();
        }


        public async Task<BookCreationDto> CreateBookAsync(BookCreationDto createBookRequest)
        {
            if (string.IsNullOrEmpty(createBookRequest.Title))
            {
                throw new Exception("Title can not be null.");
            }

            if (createBookRequest.PageNumbers <= 0)
            {
                throw new Exception("Pages can not be less than or equal zero.");
            };

            var authors = new List<Author>();

            if (createBookRequest.AuthorIds.Any())
            {
                var res = await _authorRepository.FindAuthorsByIdAsync(createBookRequest.AuthorIds);

                authors.AddRange(res);
            }

            var bookEdm = new Book
            {
                Title = createBookRequest.Title,
                Pages = createBookRequest.PageNumbers,
            };

            bookEdm.Authors = authors;


            var addedBook = await _dbContext.Set<Book>().AddAsync(bookEdm);

            await _dbContext.SaveChangesAsync();

            return new BookCreationDto
            {
                Id = addedBook.Entity.Id,
                Title = addedBook.Entity.Title,
                PageNumbers = addedBook.Entity.Pages,
                AuthorIds = bookEdm.Authors.Select(x => x.Id),
            };
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
