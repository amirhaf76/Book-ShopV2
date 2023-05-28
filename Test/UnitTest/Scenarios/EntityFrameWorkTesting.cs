using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
using BookShop.Test.UnitTest.SettingsModels;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios
{
    [Order((int)ScenariosOrder.EntityFrameWorkTesting)]
    public class EntityFrameWorkTesting : BaseTestCaseScenario
    {
        private readonly DelaySettings _delaySettings;
        private readonly ILogger<BaseTestAppScenario> _logger;

        private readonly ITestPermissionRepository _testRepository;

        public EntityFrameWorkTesting(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
            _logger = ResolveService<ILogger<BaseTestAppScenario>>();
            _delaySettings = ResolveService<DelaySettings>();

            _testRepository = ResolveService<ITestPermissionRepository>();
        }

# pragma warning disable xUnit1004
        [Fact(Skip = "Just for manual testing.")]
        public async Task SingleVsFirst()
        {
            await _testRepository.SingleVsFirst();
        }

        [Fact(Skip = "Just for manual testing.")]
        public async Task InsertAndUpdateAsync()
        {
            await _testRepository.InsertAndUpdateAsync();
        }

        [Fact(Skip = "Just for manual testing.")]
        public async Task TestEF()
        {
            await _testRepository.TestEF();
        }

        [Fact(Skip = "Just for manual testing.")]
        public void Update()
        {
            _testRepository.TestUpdate(new Permission { Id = 7, Name = "Permission_7"});
        }

        [Fact(Skip = "Just for manual testing.")]
        public void UpdateImmediately()
        {
            _testRepository.UpdateImmediately();
        }

        [Fact(Skip = "Just for manual testing.")]
        public void CheckProjectAutors()
        {
            _testRepository.CheckProjectAutors();
        }

        [Fact(Skip = "Just for manual testing.")]
        public void CheckProjectTitle()
        {
            _testRepository.CheckProjectTitle();
        }
#pragma warning restore xUnit1004

    }
}