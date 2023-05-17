using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataBaseLayer.DbContexts.BookShopDbContexts
{
    public interface IBookShopDbContext
    {
        DbContext GetDbContext();
    }
}