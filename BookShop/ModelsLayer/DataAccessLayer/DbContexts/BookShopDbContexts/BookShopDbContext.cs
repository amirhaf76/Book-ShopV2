using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
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

                buildAction
                    .HasMany(u => u.Permissions)
                    .WithMany(p => p.UserAccounts)
                    .UsingEntity<UserPermission>(
                        right => right.HasOne(rp => rp.Permission).WithMany().HasForeignKey(rp => rp.PermissionId).HasPrincipalKey(p => p.Id),
                        left => left.HasOne(rp => rp.UserAccount).WithMany().HasForeignKey(rp => rp.UserId).HasPrincipalKey(r => r.Id)
                        );
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
            });

            modelBuilder.Entity<Address>(buildAction =>
            {
                buildAction.HasKey(a => a.Id);

                buildAction
                    .HasOne(a => a.ZipCode)
                    .WithMany()
                    .HasForeignKey(a => a.ZipCodeId);
            });

            modelBuilder.Entity<Permission>(buildAction =>
            {
                buildAction.HasKey(p => p.Id);
            });

            modelBuilder.Entity<UserPermission>(buildAction =>
            {
                buildAction.HasKey(rp => new { rp.PermissionId, rp.UserId });

                buildAction
                    .HasOne(u => u.Role)
                    .WithMany()
                    .HasForeignKey(u => u.RoleId)
                    .HasPrincipalKey(r => r.Id)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Role>(buildAction =>
            {
                buildAction.HasKey(r => r.Id);

                buildAction
                    .HasMany(r => r.Permissions)
                    .WithMany(p => p.Roles)
                    .UsingEntity<RolePermission>(
                        right => right.HasOne(rp => rp.Permission).WithMany().HasForeignKey(rp => rp.PermissionId).HasPrincipalKey(p => p.Id),
                        left => left.HasOne(rp => rp.Role).WithMany().HasForeignKey(rp => rp.RoleId).HasPrincipalKey(r => r.Id),
                        join => join.HasKey(rp => new { rp.PermissionId, rp.RoleId })
                        );
            });

            modelBuilder.Entity<Stock>(buildAction =>
            {
                buildAction.HasKey(s => s.StockId);

                buildAction
                    .HasOne(s => s.Reservation)
                    .WithMany(r => r.Stocks)
                    .HasForeignKey(s => s.ReservationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                buildAction
                    .HasOne(s => s.Repository)
                    .WithMany(r => r.Stocks)
                    .HasForeignKey(s => s.RepositoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);

                buildAction
                    .HasOne(s => s.Book)
                    .WithMany(b => b.Stocks)
                    .HasForeignKey(s => s.BookId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Repository>(buildAction =>
            {
                buildAction.HasKey(r => r.Id);

                buildAction
                    .HasOne(r => r.Address)
                    .WithMany()
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasForeignKey(r => r.AddressId)
                    .IsRequired(false);

                buildAction
                    .Property(r => r.IsEnable)
                    .HasDefaultValue(false);
            });

            modelBuilder.Entity<Reservation>(buildAction =>
            {
                buildAction.HasKey(r => r.Id);

                buildAction
                    .HasOne(r => r.UserAccount)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.UserAccountId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(true);
            });

        }

       
        public DbContext GetDbContext()
        {
            return this;
        }

    }
}
