using Autofac;
using BookShop.Core.DIModule;
using BookShop.Test.UnitTest.SettingsModels;
using Infrastructure.AutoFac;
using Microsoft.Extensions.Configuration;

namespace BookShop.Test.UnitTest.Core.DIModules
{
    public class APITestDIModule : BaseDIModule<APITestDIModule>
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    return c.Resolve<IConfiguration>().GetSection("delaySettings").Get<DelaySettings>();
                })
                .InstancePerLifetimeScope()
                .AsSelf();

            builder.AddBookShopDIModule();

            base.Load(builder);
        }

    }
}
