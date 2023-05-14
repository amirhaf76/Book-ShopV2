using Autofac;
using BookShop.ModelsLayer.DataBaseLayer.DbContexts.BookShopDbContexts;
using Infrastructure.AutoFac;
using Microsoft.EntityFrameworkCore;

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
}
