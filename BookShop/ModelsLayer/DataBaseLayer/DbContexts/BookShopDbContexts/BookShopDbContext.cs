using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataBaseLayer.DbContexts.BookShopDbContexts
{
    public class BookShopDbContext : DbContext, IBookShopDbContext, IScope
    {
        public BookShopDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(buildAction =>
            {
                buildAction.HasKey(b => b.Id);
            });

            modelBuilder.Entity<Author>(buildAction =>
            {
                buildAction.HasKey(a => a.Id);

                buildAction
                    .HasMany(author => author.Books)
                    .WithMany(book => book.Authors)
                    .UsingEntity<AuthorBook>(
                        "AuthorBooks",
                        right => right.HasOne(b => b.Book).WithMany().HasForeignKey(b => b.BookId).HasPrincipalKey(b => b.Id),
                        left => left.HasOne(a => a.Author).WithMany().HasForeignKey(a => a.AuthorId).HasPrincipalKey(a => a.Id),
                        join => join.HasKey(joined => new { joined.BookId, joined.AuthorId })
                        );
            });

            modelBuilder.Entity<UserAccount>(buildAction =>
            {
                buildAction.HasKey(u => u.Id);
            });

            modelBuilder.Entity<Country>(buildAction =>
            {
                buildAction.HasKey(c => c.Id);

            });

            modelBuilder.Entity<Province>(buildAction =>
            {
                buildAction.HasKey(p => p.Id);

                buildAction
                    .HasOne(p => p.Country)
                    .WithMany(c => c.Provinces)
                    .HasForeignKey(p => p.CountryId);
            });

            modelBuilder.Entity<City>(buildAction =>
            {
                buildAction.HasKey(c => c.Id);

                buildAction
                    .HasOne(c => c.Province)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(c => c.ProvinceId);
            });

            modelBuilder.Entity<ZipCode>(buildAction =>
            {
                buildAction.HasKey(z => z.Id);

                buildAction.HasIndex(z => z.Code).IsUnique();

                buildAction
                    .HasOne(z => z.City)
                    .WithMany()
                    .HasForeignKey(z => z.CityId);

                buildAction
                    .HasOne(z => z.Province)
                    .WithMany()
                    .HasForeignKey(z => z.ProvinceId);

                buildAction
                    .HasOne(z => z.Country)
                    .WithMany()
                    .HasForeignKey(z => z.CountryId);
            });

            modelBuilder.Entity<Address>(buildAction =>
            {
                buildAction.HasKey(a => a.Id);

                buildAction
                    .HasOne(a => a.ZipCode)
                    .WithMany()
                    .HasForeignKey(a => a.ZipCodeId);
            });


        }

        public DbContext GetDbContext()
        {
            return this;
        }

    }
}
