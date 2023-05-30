using Autofac;
using BookShop.Core.DIModule;
using BookShop.ModelsLayer.DataAccessLayer.DbContexts.BookShopDbContexts;
using BookShop.Test.UnitTest.SettingsModels;
using Infrastructure.AutoFac;
using Microsoft.EntityFrameworkCore;
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
                    var config = new ConfigurationBuilder()
                        .SetBasePath(Environment.CurrentDirectory)
                        .AddJsonFile("appsettings.json")
                        .Build();

                    return (IConfiguration)config;
                })
                .InstancePerLifetimeScope()
                .AsSelf();

            builder
                .Register(c =>
                {
                    return c.Resolve<IConfiguration>().GetSection("delaySettings").Get<DelaySettings>();
                })
                .InstancePerLifetimeScope()
                .AsSelf();

            builder
                .Register(c =>
                {
                    var config = c.Resolve<IConfiguration>();

                    var contextOptions = new DbContextOptionsBuilder<BookShopDbContext>().UseSqlServer(config.GetConnectionString("BookShopDB"));

                    return contextOptions.Options;
                })
                .InstancePerLifetimeScope()
                .AsSelf();

            builder.AddBookShopDIModule();

            

            base.Load(builder);
        }

    }
}
