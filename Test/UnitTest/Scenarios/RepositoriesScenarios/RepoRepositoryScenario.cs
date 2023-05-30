using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios.RepositoriesScenarios
{
    [Order((int)RepositoryTestCollectionScenarioOrder.RepoRepositoryScenario)]
    [Collection(nameof(CollectionTestOrder.Repository))]
    public class RepoRepositoryScenario : BaseTestCaseScenario
    {
        private readonly ILogger<BaseTestAppScenario> _logger;
        private readonly IStockRepository _stockRepository;

        public RepoRepositoryScenario(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
            _logger = ResolveService<ILogger<BaseTestAppScenario>>();
            _stockRepository = ResolveService<IStockRepository>();
        }


    }


}