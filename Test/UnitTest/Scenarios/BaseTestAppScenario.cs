using Autofac.Extras.Moq;
using BookShop.Test.UnitTest;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
using BookShop.Test.UnitTest.SettingsModels;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios
{
    [Order((int)ScenariosOrder.BaseTestAppScenario)]
    public class BaseTestAppScenario : BaseTestCaseScenario
    {
        private readonly DelaySettings _delaySettings;
        private readonly ILogger<BaseTestAppScenario> _logger;

        public BaseTestAppScenario(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
            _logger = ResolveService<ILogger<BaseTestAppScenario>>();
            _delaySettings = ResolveService<DelaySettings>();
        }

        [Fact]
        public void TestSampleTestCase()
        {

            _delaySettings.Should().NotBeNull();

            _delaySettings.ShortDelay.Should().NotBe(TimeSpan.Zero);
            _delaySettings.MediumDelay.Should().NotBe(TimeSpan.Zero);
            _delaySettings.LongDelay.Should().NotBe(TimeSpan.Zero);

            _logger.LogInformation("{@delaySettings}", _delaySettings);
            _logger.LogDebug("Simple test run correctly.");

        }

        [Fact]
        public void TestSampleTestCase2()
        {
            using var mock = AutoMock.GetLoose();


            mock.Mock<IFoo>().Setup(foo => foo.DoSomething("ping")).Returns(true);
            var foo = mock.Create<IFoo>();

            foo.DoSomething("ping").Should().BeTrue();


        }

        [Fact]
        public void TestSampleTestCase3()
        {
            using var mock = AutoMock.GetStrict();

            mock.Mock<IFoo>().Setup(foo => foo.DoSomething("ping")).Returns(true);
            var foo = mock.Create<IFoo>();

            foo.GetCount();



        }

    }
}