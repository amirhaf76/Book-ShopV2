using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class BookService : IBookService, IScope
    {
        private readonly ILogger<BookService> _logger;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IExceptionCaseService _exceptionCaseService;

        public BookService(ILogger<BookService> logger, IAuthorRepository authorRepository, IBookRepository bookRepository, IExceptionCaseService exceptionCaseService)
        {
            _logger = logger;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _exceptionCaseService = exceptionCaseService;
        }


        public async Task RemoveBookAsync(int id)
        {
            var theBook = await _bookRepository.FindAsync(id);

            _bookRepository.Remove(theBook);

            await _bookRepository.SaveChangesAsync();
        }

        public async Task<BookCreationDto> CreateBookAsync(BookCreationDto bookCreationDto)
        {
            ValidateBookTitle(bookCreationDto.Title);

            ValidateBookPageNumbers(bookCreationDto.PageNumbers);

            var authors = new List<Author>();

            if (bookCreationDto.AuthorIds.Any())
            {
                var res = await _authorRepository.FindAuthorsByIdAsync(bookCreationDto.AuthorIds);

                authors.AddRange(res);
            }

            var book = new Book
            {
                Title = bookCreationDto.Title,
                Pages = bookCreationDto.PageNumbers,
                PublishedDate = bookCreationDto.PublishedDate,
            };

            book.Authors = authors;

            var addedBook = await _bookRepository.AddAsync(book);

            await _bookRepository.SaveChangesAsync();

            return addedBook.ConvertToBookCreationDto();
        }

        public async Task<BookUpdateDto> UpdateBookAsync(BookUpdateDto bookUpdateDto)
        {
            ValidateBookTitle(bookUpdateDto.Title);

            ValidateBookPageNumbers(bookUpdateDto.PageNumbers);

            var theBook = await _bookRepository.GetBookWithItsAuthorsAsync(bookUpdateDto.Id);

            if (theBook == null)
            {
                throw _exceptionCaseService.CreateBookNotFoundException(bookUpdateDto.Id);
            }

            theBook = await UpdateBookAsync(theBook, bookUpdateDto);

            _bookRepository.Update(theBook);

            await _bookRepository.SaveChangesAsync();

            return theBook.ConvertToBookUpdateDto();
        }


        public async Task<Book> UpdateBookAsync(Book theBook, BookUpdateDto bookUpdateDto)
        {
            var newAuthorIds = await UpdateAuthorsAsync(theBook.Authors, bookUpdateDto.AuthorIds);

            theBook.UpdateBook(bookUpdateDto, newAuthorIds.ToList());

            return theBook;
        }

        public async Task<IEnumerable<Author>> UpdateAuthorsAsync(IEnumerable<Author> currentAuthors, IEnumerable<int> desiredAuthorIds)
        {
            var intersectedAuthors = currentAuthors.IntersectBy(desiredAuthorIds, author => author.Id);

            var newAuthorIds = desiredAuthorIds.ExceptBy(currentAuthors.Select(author => author.Id), id => id);

            var intendedAuthors = new List<Author>();

            if (newAuthorIds.Any())
            {
                var res = await _authorRepository.FindAuthorsByIdAsync(newAuthorIds);

                intendedAuthors.AddRange(res);
            }

            return intendedAuthors.UnionBy(intersectedAuthors, author => author.Id);
        }


        private static void ValidateBookPageNumbers(int pageNumbers)
        {
            if (pageNumbers <= 0)
            {
                throw new ArgumentOutOfRangeException("PageNumbers can not be less than or equal zero!");
            };
        }

        private static void ValidateBookTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException($"Book title can not be null!");
            }
        }
    }
}
