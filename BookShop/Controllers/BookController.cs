using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.Dtos.FilterDtos;
using BookShop.ModelsLayer.DtosExtension;
using BookShop.ModelsLayer.GetwayLayer.RequestResponseModels;
using BookShop.ModelsLayer.ViewModelLayer.VMExtension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        private readonly IBookRepository _bookRepository;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookRepository bookRepository, IBookService bookService)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _bookService = bookService;
        }

        [Authorize]
        [HttpPost("Book")]
        public async Task<BookCreationResponse> CreateBookAsync([FromBody] BookCreationRequest createBookRequest)
        {
            var bookCreationResult = await _bookService.CreateBookAsync(createBookRequest.ConvertToBookCreationDto());

            return bookCreationResult.ConvertToBookCreationResponse();
        }


        [AllowAnonymous]
        [HttpDelete("Book")]
        public async Task RemoveBookAsync([FromQuery] int id)
        {
            await _bookService.RemoveBookAsync(id);
        }


        [AllowAnonymous]
        [HttpPut("Book")]
        public async Task<BookUpdateResponse> UpdateBookAsync([FromBody] BookUpdateRequest bookUpdateRequest)
        {
            var bookUpdateResult = await _bookService.UpdateBookAsync(bookUpdateRequest.ConvertToBookUpdateRequest());

            return bookUpdateResult.ConvertToBookUpdateResponse();
        }

        [AllowAnonymous]
        [HttpGet("Book")]
        public async Task<IEnumerable<BookQueryResponse>> GetAllBooks([FromQuery] GetAllBooksRequest getAllBooksRequest)
        {
            var paginationFilter = new PaginationFilter
            {
                PageNumber = getAllBooksRequest.PageNumber,
                PageSize = getAllBooksRequest.PageSize,
            };

            var bookFilterDto = new BookFilterDto
            {
                Id = getAllBooksRequest.Id,
                Title = getAllBooksRequest.Title,
                PublishedYear = getAllBooksRequest.PublishedYear,
            };

            if (!paginationFilter.Validate())
            {
                throw new Exception("Pagination has a problem");
            }

            var receivedBook = await _bookRepository.GetAllBooksAsync(paginationFilter, bookFilterDto);

            _logger.LogInformation("{@book}", receivedBook);

            return receivedBook.Select(book => book.ConvertToBookQueryResponse());
        }
    }


}
