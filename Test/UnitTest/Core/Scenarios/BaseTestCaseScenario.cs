using Autofac;
using Autofac.Core;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Autofac.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Core.Scenarios
{
    public class BaseTestCaseScenario : IAssemblyFixture<AppConfiguration>, IDisposable
    {
        private readonly ILifetimeScope _scope;

        protected readonly AppConfiguration _totalConfiguration;

        public BaseTestCaseScenario(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper)
        {
            _totalConfiguration = totalConfiguration;

            _scope = _totalConfiguration.CreateScope(builder =>
            {
                var config = new ConfigurationBuilder()
                               .SetBasePath(Environment.CurrentDirectory)
                               .AddJsonFile("appsettings.json")
                               .Build();
                //builder.RegisterInstance<IConfiguration>(config);

                builder.RegisterSerilog(new LoggerConfiguration().ReadFrom.Configuration(config).WriteTo.TestOutput(testOutputHelper));
            });
        }


        protected T ResolveService<T>()
        {
            return _scope.Resolve<T>();
        }

        protected T ResolveService<T>(params Parameter[] parameters)
        {
            return _scope.Resolve<T>(parameters);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _scope.Dispose();
        }
    }
}
