using BookShop.Models.DataBaseLayer.DataModelRepository;
using BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.Models.DtosExtension;
using BookShop.Models.GetwayLayer.RequestResponseModels;
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

        public BookController(ILogger<BookController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        [Authorize]
        [HttpPost("Book")]
        public async Task<BookCreationResponse> CreateBookAsync([FromBody] BookCreationRequest createBookRequest)
        {
            var bookCreationResult = await _bookRepository.CreateBookAsync(createBookRequest.ConvertToBookCreationDto());

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
