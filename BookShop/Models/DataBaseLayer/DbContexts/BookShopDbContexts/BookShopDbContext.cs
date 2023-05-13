using BookShop.Models.DataBaseLayer.DataBaseModels;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;

namespace BookShop.Models.DataBaseLayer.DbContexts.BookShopDbContexts
{
    public class BookShopDbContext : DbContext, IBookShopDbContext, IScope
    {

        public BookShopDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Author>()
                .HasMany(author => author.Books)
                .WithMany(book => book.Authors)
                .UsingEntity<AuthorBook>();


        }

        public DbContext GetDbContext()
        {
            return this;
        }

        public DbSet<Book> Books { get; set; }


        public DbSet<UserAccount> UserAccounts { get; set; }


        public DbSet<User> Users { get; set; }

        public DbSet<Author> Authors { get; set; }


        public DbSet<Address> Addresses { get; set; }

        public DbSet<ZipCode> ZipCodes { get; set; }


        public DbSet<Country> Countries { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<AuthorBook> AuthorBooks { get; set; }


    }
}
