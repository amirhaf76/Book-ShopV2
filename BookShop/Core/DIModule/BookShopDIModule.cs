using Autofac;
using BookShop.ModelsLayer.DataAccessLayer.DbContexts.BookShopDbContexts;
using Infrastructure.AutoFac;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookShop.Core.DIModule
{
    public class BookShopDIModule : BaseDIModule<BookShopDIModule>
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register<DbContext>(c => c.Resolve<BookShopDbContext>())
                .AsSelf();
                
            base.Load(builder);
        }
    }

    public static class BookShopDIModuleExtension
    {
        public static ContainerBuilder AddBookShopDIModule(this ContainerBuilder builder)
        {
            var assemply = Assembly.GetAssembly(typeof(BookShopDIModule));

            builder.RegisterAssemblyModules(assemply);

            return builder;
        }
    }
}
