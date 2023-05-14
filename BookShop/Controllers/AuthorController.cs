using BookShop.Models.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.ModelsLayer.Dtos.AuthorDtos;
using BookShop.ModelsLayer.GetwayLayer.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IAuthorService _authorService;

        public AuthorController(ILogger<BookController> logger, IAuthorService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }


        [HttpPost("Author")]
        public async Task CreateAuthorAsync([FromBody] CreateAuthorRequest authorCreationRequest)
        {
            var authorSubRequests = authorCreationRequest
                .AuthorInfos
                .DistinctBy(x => x.FirstName + x.LastName)
                .Select(x => new AuthorDto { FirstName = x.FirstName, LastName = x.LastName });

            foreach (var authorReqeust in authorSubRequests)
            {
                await _authorService.AddAuthorIfItDoesntExistAsync(authorReqeust);
            }
        }
    }


}
