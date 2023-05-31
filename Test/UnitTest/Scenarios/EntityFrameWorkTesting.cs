using BookShop.ModelsLayer.DataAccessLayer.DataModelRepository;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
using BookShop.Test.UnitTest.Core.Scenarios.CollectionAndTestCaseOrders;
using BookShop.Test.UnitTest.SettingsModels;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios
{
    [Order((int)DefaultTestCollectionScenarioOrder.EntityFrameWorkTesting)]
    [Collection(nameof(CollectionTestOrder.Default))]
    public class EntityFrameWorkTesting : BaseTestCaseScenario
    {
        private readonly DelaySettings _delaySettings;
        private readonly ILogger<BaseTestAppScenario> _logger;

        private readonly TestPermissionRepository _testRepository;

        public EntityFrameWorkTesting(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
            _logger = ResolveService<ILogger<BaseTestAppScenario>>();
            _delaySettings = ResolveService<DelaySettings>();

            _testRepository = ResolveService<TestPermissionRepository>();
        }

# pragma warning disable xUnit1004
        [Fact()]
        public async Task FastTesting()
        {
            await _testRepository.TestEF16();
        }
#pragma warning restore xUnit1004

    }
}