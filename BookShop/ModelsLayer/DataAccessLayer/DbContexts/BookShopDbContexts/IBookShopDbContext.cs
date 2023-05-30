using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DbContexts.BookShopDbContexts
{
    public interface IBookShopDbContext
    {
        DbContext GetDbContext();
    }
}