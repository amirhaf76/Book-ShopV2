using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.RequestResponseModels;
using BookShop.ModelsLayer.ViewModelLayer.VMExtension;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RepositoryController : ControllerBase
    {
        private readonly ILogger<RepositoryController> _logger;
        private readonly IRepositoryService _repositoryService;

        public RepositoryController(ILogger<RepositoryController> logger, IRepositoryService repositoryService)
        {
            _logger = logger;
            _repositoryService = repositoryService;
        }


        [HttpGet("Repository")]
        public async Task<IEnumerable<RepositoryVM>> GetRepositoryAsync([FromQuery]GettingRepositoriesFilterVM gettingRepositoriesFilterVM)
        {
            var repository = await _repositoryService.GetRepositoriesAsync(gettingRepositoriesFilterVM.ConvertToGettingRepositoriesFilter());

            return repository.Select(r => r.ConvertToRepositoryVM());

        }

        
    }


}
