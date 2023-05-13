using Microsoft.EntityFrameworkCore;

namespace BookShop.Models.DataBaseLayer.DbContexts.BookShopDbContexts
{
    public interface IBookShopDbContext
    {
        DbContext GetDbContext();
    }
}