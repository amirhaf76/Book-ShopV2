using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DbContexts.BookShopDbContexts
{
    public sealed class BookShopDbContext : DbContext, IBookShopDbContext, IScope
    {
        private readonly ILogger<BookShopDbContext> _logger;

        public BookShopDbContext(DbContextOptions<BookShopDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateAddressModel(modelBuilder);

            CreateAuthorModel(modelBuilder);

            CreateBookModel(modelBuilder);

            CreateCityModel(modelBuilder);

            CreateCountryModel(modelBuilder);

            CreatePermissionModel(modelBuilder);

            CreateProvinceModel(modelBuilder);

            CreateRepositoryModel(modelBuilder);

            CreateReservationModel(modelBuilder);

            CreateRoleModel(modelBuilder);

            CreateStockModel(modelBuilder);

            CreateUserAccountModel(modelBuilder);

            CreateUserPermissionModel(modelBuilder);

            CreateZipCodeModel(modelBuilder);
        }


        private static void CreateCountryModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(buildAction =>
            {
                buildAction.HasKey(c => c.Id);

            });
        }

        private static void CreateUserAccountModel(ModelBuilder modelBuilder)
        {
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
        }

        private static void CreateBookModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(buildAction =>
            {
                buildAction.HasKey(b => b.Id);
            });
        }

        private static void CreateAuthorModel(ModelBuilder modelBuilder)
        {
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
        }

        private static void CreateProvinceModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Province>(buildAction =>
            {
                buildAction.HasKey(p => p.Id);

                buildAction
                    .HasOne(p => p.Country)
                    .WithMany(c => c.Provinces)
                    .HasForeignKey(p => p.CountryId);
            });
        }

        private static void CreateCityModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(buildAction =>
            {
                buildAction.HasKey(c => c.Id);

                buildAction
                    .HasOne(c => c.Province)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(c => c.ProvinceId);
            });
        }

        private static void CreateZipCodeModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ZipCode>(buildAction =>
            {
                buildAction.HasKey(z => z.Id);

                buildAction.HasIndex(z => z.Code).IsUnique();

                buildAction
                    .HasOne(z => z.City)
                    .WithMany()
                    .HasForeignKey(z => z.CityId);
            });
        }

        private static void CreateAddressModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(buildAction =>
            {
                buildAction.HasKey(a => a.Id);

                buildAction
                    .HasOne(a => a.ZipCode)
                    .WithMany()
                    .HasForeignKey(a => a.ZipCodeId);
            });
        }

        private static void CreatePermissionModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>(buildAction =>
            {
                buildAction.HasKey(p => p.Id);
            });
        }

        private static void CreateUserPermissionModel(ModelBuilder modelBuilder)
        {
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
        }

        private static void CreateRoleModel(ModelBuilder modelBuilder)
        {
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
        }

        private static void CreateStockModel(ModelBuilder modelBuilder)
        {
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
        }

        private static void CreateRepositoryModel(ModelBuilder modelBuilder)
        {
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
        }

        private static void CreateReservationModel(ModelBuilder modelBuilder)
        {
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
