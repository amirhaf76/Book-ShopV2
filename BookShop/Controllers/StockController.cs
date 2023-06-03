using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.StockVMs;
using BookShop.ModelsLayer.ViewModelLayer.VMExtension;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StockController: ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stockService;

        public StockController(ILogger<StockController> logger, IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }


        [HttpPost("Stock")]
        public async Task<StockCreationResultVM> StockBookAsync([FromBody] StockCreationVM stockCreationVM)
        {
            var stockCreationResult = await _stockService.StockBookAsync(stockCreationVM.ConvertToStockingBookDto());

            return stockCreationResult.ConvertToStockCreationResultVM();
        }


    }
}
