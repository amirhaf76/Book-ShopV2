using BookShop.Models.DataBaseLayer.DataBaseModels;
using BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.Models.GetwayLayer.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        private readonly IAuthorRepository _authorRepository;

        public AuthorController(ILogger<BookController> logger, IAuthorRepository authorRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;
        }


        [HttpPost("Author")]
        public async Task CreateAuthorAsync([FromBody] CreateAuthorRequest authorCreationRequest)
        {
            var authorSubRequests = authorCreationRequest
                .AuthorInfos
                .DistinctBy(x => x.FirstName + x.LastName)
                .Select(x => new Author { FirstName = x.FirstName, LastName = x.LastName });

            foreach (var authorReqeust in authorSubRequests)
            {
                await _authorRepository.AddAuthorIfItDoesntExistAsync(authorReqeust);
            }
        }
    }


}
