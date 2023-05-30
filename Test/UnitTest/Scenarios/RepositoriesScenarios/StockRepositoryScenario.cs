using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios.RepositoriesScenarios
{
    [Order((int)RepositoryTestCollectionScenarioOrder.StockRepositoryScenario)]
    [Collection(nameof(CollectionTestOrder.Repository))]
    public class StockRepositoryScenario : BaseTestCaseScenario
    {
        private readonly ILogger<BaseTestAppScenario> _logger;
        private readonly IStockRepository _stockRepository;

        public StockRepositoryScenario(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
            _logger = ResolveService<ILogger<BaseTestAppScenario>>();
            _stockRepository = ResolveService<IStockRepository>();
        }

        [Fact]
        public async Task TestSampleTestCase()
        {
            var stock = await _stockRepository.GetStocksAsync(new StockFilter
            {
                StockIds = new long[] { 1L, 2L }
            });

            stock.Should().HaveCount(2);
            stock.Should().BeEquivalentTo(new[]
            {
                new { StockId = 1L },
                new { StockId = 2L },
            });
        }

        [Fact]
        public async Task TestSampleTestCase2()
        {
            var stock = await _stockRepository.AddAsync(new Stock
            {
                BookId = 12,
                RepositoryId = 1,
                Status = StockStatus.New,
            });

            await _stockRepository.SaveChangesAsync();

            stock.StockId.Should().NotBe(default);
        }
    }


}