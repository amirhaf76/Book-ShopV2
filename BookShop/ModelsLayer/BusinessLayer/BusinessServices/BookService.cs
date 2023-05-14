using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Dtos.BookDtos;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServices
{
    public class BookService : IBookService, IScope
    {
        private readonly ILogger<BookService> _logger;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public BookService(ILogger<BookService> logger, IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
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

            var book = new Book
            {
                Title = createBookRequest.Title,
                Pages = createBookRequest.PageNumbers,
            };

            book.Authors = authors;

            var addedBook = await _bookRepository.AddAsync(book);

            await _bookRepository.SaveChangesAsync();

            return new BookCreationDto
            {
                Id = addedBook.Entity.Id,
                Title = addedBook.Entity.Title,
                PageNumbers = addedBook.Entity.Pages,
                AuthorIds = book.Authors.Select(x => x.Id),
            };
        }
    }
}
