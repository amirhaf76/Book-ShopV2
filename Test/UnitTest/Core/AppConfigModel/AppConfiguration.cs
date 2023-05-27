using Autofac;
using BookShop.Test.UnitTest.Core.DIModules;

namespace BookShop.Test.UnitTest.Core.AppConfigModel
{
    public class AppConfiguration
    {
        private readonly IContainer _container;


        public AppConfiguration()
        {
            var builder = new ContainerBuilder();

            _container = RegisterDependencies(builder).Build();
        }

        private static ContainerBuilder RegisterDependencies(ContainerBuilder builder)
        {
            var apiTestDIModule = typeof(APITestDIModule).Assembly;

            builder.RegisterAssemblyModules(apiTestDIModule);

            return builder;
        }

        public ILifetimeScope CreateScope()
        {
            return _container.BeginLifetimeScope();
        }

        public ILifetimeScope CreateScope(Action<ContainerBuilder> action)
        {
            return _container.BeginLifetimeScope(action);
        }
    }
}
