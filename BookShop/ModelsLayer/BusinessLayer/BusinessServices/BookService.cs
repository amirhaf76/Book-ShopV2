using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.DtosExtension;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServices
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

        public async Task<BookCreationDto> CreateBookAsync(BookCreationDto bookCreationDto)
        {
            if (string.IsNullOrEmpty(bookCreationDto.Title))
            {
                throw new ArgumentNullException($"Title can not be null in '{nameof(bookCreationDto)}' parameter!");
            }

            if (bookCreationDto.PageNumbers <= 0)
            {
                throw new ArgumentOutOfRangeException("PageNumbers can not be less than or equal zero!");
            };

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
            if (string.IsNullOrEmpty(bookUpdateDto.Title))
            {
                throw new ArgumentNullException($"Title can not be null in '{nameof(bookUpdateDto)}' parameter!");
            }

            if (bookUpdateDto.PageNumbers <= 0)
            {
                throw new ArgumentOutOfRangeException("PageNumbers can not be less than or equal zero!");
            }

            
            var theBook = await _bookRepository.GetAndLoadBookAsync(bookUpdateDto.Id);

            if (theBook == null)
            {
                throw _exceptionCaseService.CreateBookNotFoundException(bookUpdateDto.Id);
            }

            var intersectedAuthors = theBook.Authors.IntersectBy(bookUpdateDto.AuthorIds, author => author.Id);
            var newAuthorIds = bookUpdateDto.AuthorIds.ExceptBy(theBook.Authors.Select(author => author.Id), id => id);

            var intendedAuthors = new List<Author>();

            foreach(var newAuthorId in newAuthorIds)
            {
                var receivedAuthor = await _authorRepository.FindAuthorAsync(newAuthorId);

                if (receivedAuthor == null)
                {
                    throw _exceptionCaseService.CreateAuthorNotFoundException(newAuthorId);
                }

                intendedAuthors.Add(receivedAuthor);
            }

            var trackedNewAuthorIds = intendedAuthors.UnionBy(intersectedAuthors, author => author.Id);

            theBook.Title = bookUpdateDto.Title;
            theBook.Pages = bookUpdateDto.PageNumbers;
            theBook.PublishedDate = bookUpdateDto.PublishedDate;
            theBook.Authors = trackedNewAuthorIds.ToList();

            _bookRepository.Update(theBook);
       

            await _bookRepository.SaveChangesAsync();

            return theBook.ConvertToBookUpdateDto();
        }
    }
}
