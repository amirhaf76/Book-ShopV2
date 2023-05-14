using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Dtos.FilterDtos;
using BookShop.ModelsLayer.DtosExtension;
using BookShop.ModelsLayer.GetwayLayer.RequestResponseModels;
using Infrastructure.Tools;
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
        [HttpGet("AllBooks")]
        public async Task<IEnumerable<BookQueryResponse>> GetAllBooks([FromQuery] PaginationFilter paginationFilter)
        {
            if (!paginationFilter.Validate())
            {
                throw new Exception("Pagination has a problem");
            }

            var receivedBook = await _bookRepository.GetAllBooksAsync(paginationFilter);

            _logger.LogInformation("{@book}", receivedBook);

            return receivedBook.Select(BookDtosExtension.ConvertToBookQueryResponse);
        }
    }


}
