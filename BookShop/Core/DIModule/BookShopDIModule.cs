using Autofac;
using Infrastructure.AutoFac;

namespace BookShop.Core.DIModule
{
    public class BookShopDIModule : BaseDIModule<BookShopDIModule>
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
