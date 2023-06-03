using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DbContexts.BookShopDbContexts;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class TestPermissionRepository : BaseRepository<Permission>, IScope
    {
        private readonly ILogger<TestPermissionRepository> _logger;

        private static BookShopDbContext _dbContext;


        public TestPermissionRepository(ILogger<TestPermissionRepository> logger, IConfiguration configuration) :
            base(CreateDbContext(configuration))
        {
            _logger = logger;
        }


        private static BookShopDbContext CreateDbContext(IConfiguration configuration)
        {
            var option = new DbContextOptionsBuilder<BookShopDbContext>().UseSqlServer(configuration.GetConnectionString("BookShopDB") ??
                    throw new InvalidOperationException("Connection string 'BookShopDB' not found.")).Options;

            _dbContext = new BookShopDbContext(option);

            return _dbContext;
        }


        public async Task SingleVsFirst()
        {
            var permission = await _dbSet.Where(q => q.Name == "book_r").FirstOrDefaultAsync();
            _logger.LogDebug("{@permission}", permission);

            permission = await _dbSet.Where(q => q.Name == "book_r").SingleOrDefaultAsync();
            _logger.LogDebug("{@permission}", permission);

            permission = await _dbSet.Where(q => EF.Functions.Like(q.Name, "book_%")).SingleAsync(); // Error
            _logger.LogDebug("{@permission}", permission);

            // SELECT TOP(1) [p].[Id], [p].[Name]
            // FROM[Permission] AS[p]
            // WHERE[p].[Name] = N'book_r'

            // SELECT TOP(2) [p].[Id], [p].[Name]
            // FROM[Permission] AS[p]
            // WHERE[p].[Name] = N'book_r'

            // SELECT TOP(2) [p].[Id], [p].[Name]
            // FROM[Permission] AS[p]
            // WHERE[p].[Name] LIKE N'book_%'
        }

        public async Task InsertAndUpdateAsync()
        {
            var data = await _dbSet.AddAsync(new Permission() { Name = "Permission_1" });
            _logger.LogDebug("{@data}", data);

            await SaveChangesAsync();

            var permission = await _dbSet.Where(q => q.Name == "Permission_1").FirstOrDefaultAsync();
            _logger.LogDebug("{@permission}", permission);

            permission.Name = "Updated_permission_1";

            _logger.LogDebug("{@permission}", permission);
            await SaveChangesAsync();

            // https://stackoverflow.com/questions/641120/what-does-exec-sp-reset-connection-mean-in-sql-server-profiler
            // sp_reset_connection does NOT reset the transaction isolation level to the server default from the previous connection's setting.
            // Starting with SQL 2014, for client drivers with TDS version 7.3 or higher, the transaction isolation levels will be reset back to the default.
            // ACID principle.

            // exec sp_executesql N'SET IMPLICIT_TRANSACTIONS OFF;
            // SET NOCOUNT ON;
            // INSERT INTO[Permission] ([Name])
            // OUTPUT INSERTED.[Id]
            // VALUES(@p0);
            // ',N'@p0 nvarchar(4000)',@p0=N'Permission_1'

            // exec sp_reset_connection 

            // SELECT TOP(1) [p].[Id], [p].[Name]
            // FROM[Permission] AS[p]
            // WHERE[p].[Name] = N'Permission_1'

            // exec sp_reset_connection 

            // exec sp_executesql N'SET IMPLICIT_TRANSACTIONS OFF;
            // SET NOCOUNT ON;
            // UPDATE[Permission] SET[Name] = @p0
            // OUTPUT 1
            // WHERE[Id] = @p1;
            // ',N'@p1 int, @p0 nvarchar(4000)',@p1=5,@p0=N'Updated_permission_1'
        }


        public void TestUpdate(Permission permission)
        {
            var data = _dbContext.Update(permission);
            _logger.LogDebug("{@data}", data.Entity);

            _dbContext.ChangeTracker.TrackGraph(permission, e =>
            {
                e.Entry.State = EntityState.Deleted;
            });


            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                _logger.LogDebug("Entity {0} {1}", entry.Entity.GetType().Name, entry.State.ToString());
            }

            _dbContext.SaveChanges();
        }

        public int UpdateImmediately()
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            return _dbContext
                .Set<Permission>()
                .Where(x => EF.Functions.Like(x.Name, "Permission_7"))
                .ExecuteUpdate(setter => setter.SetProperty(p => p.Name, "Hello"));
        }

        public void CheckProjectAutors()
        {
            var booksWithGetAll = _dbContext.Set<Book>().ToList();
            _logger.LogDebug("{@books}", booksWithGetAll.Select(x => x.Authors));


            var booksWithoutGetAll = _dbContext.Set<Book>().Select(x => x.Authors).ToList();
            _logger.LogDebug("{@books}", booksWithoutGetAll);
        }

        public void CheckProjectTitle()
        {
            var booksWithGetAll = _dbContext.Set<Book>().ToList();
            _logger.LogDebug("{@books}", booksWithGetAll.Select(x => x.Title));


            var booksWithoutGetAll = _dbContext.Set<Book>().Select(x => x.Title).ToList();
            _logger.LogDebug("{@books}", booksWithoutGetAll);
        }


        public async Task TestEF()
        {
            var data = await _dbContext.AddAsync(new Permission() { Id = 7, Name = "Permission_2" });
            _logger.LogDebug("{@data}", data.Entity);

            _dbContext.SaveChanges();
        }

        public async Task TestEF2()
        {
            var queriable = _dbContext
                .Set<Book>()
                .Where(q => q.Pages % 2 == 0)
                .Where(q => q.Pages % 2 == 0)
                .Include(b => b.Authors)
                .SelectMany(b => b.Authors)
                .Skip(10)
                .Take(5)
                .AsSplitQuery();

            _logger.LogDebug(queriable.ToQueryString());

            await queriable.ToListAsync();
        }

        public async Task TestEF3()
        {
            var queriable = _dbContext.Set<Book>()
                .Where(q => q.Pages % 2 == 0 &&
                            q.Pages % 2 == 1)
                //.Include(b => b.Authors)
                .SelectMany(b => b.Authors)
                .Skip(10)
                .Take(5)
                .AsSplitQuery();

            _logger.LogDebug(queriable.ToQueryString());

            await queriable.ToListAsync();
        }

        public async Task TestEF4()
        {
            var queriable = _dbContext
                .Set<Book>()
                .OrderByDescending(q => q.Id)
                .Include(b => b.Authors)
                .Skip(2)
                .Take(5)
                //.SelectMany(b => b.Authors)
                .AsSplitQuery();

            _logger.LogDebug(queriable.ToQueryString());

            var t = await queriable.ToListAsync();

            var c = t.FirstOrDefault().Stocks;
        }

        public async Task TestEF5()
        {
            var q = _dbContext
                .Set<Book>()
                .OrderByDescending(q => q.Id)
                .Take(20);

            var queriable = q
                .OrderByDescending(q => q.Id)
                .Skip(2)
                .Take(5)
                .Include(b => b.Authors)
                .AsSplitQuery();

            _logger.LogDebug(queriable.ToQueryString());

            var t = await queriable.ToListAsync();

        }

        public async Task TestEF6()
        {
            var q = _dbContext
                .Set<Book>()
                .OrderByDescending(q => q.Id)
                .Skip(1)
                .Take(20);

            var queriable = q
                .OrderByDescending(q => q.Id)
                .Skip(2)
                .Take(5)
                .Include(b => b.Authors)
                .AsSplitQuery();

            _logger.LogDebug(queriable.ToQueryString());

            var t = await queriable.ToListAsync();

        }

        public async Task TestEF7()
        {
            var book = new Book
            {
                Id = 12,
                Pages = 4322,
            };

            // var t = _dbContext.Attach(book).Property(x => x.Pages);


            _dbContext.ChangeTracker.TrackGraph(book, book, e =>
           {
               e.Entry.Property(nameof(book.Pages)).IsModified = true;

               return false;
           });

            await _dbContext.SaveChangesAsync();
        }

        public async Task TestEF8()
        {
            var book = new Book
            {
                Id = 12,
                Pages = 4322,
            };

            // var t = _dbContext.Attach(book).Property(x => x.Pages);


            var theBook = _dbContext.Set<Book>().Where(q => q.Id == 12).AsNoTracking().FirstOrDefault();

            theBook.Pages = 433;

            _dbContext.Attach(theBook).Property(b => b.Pages).IsModified = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task TestEF9()
        {
            var book = new Book
            {
                Id = 454646,
                Pages = 852,
            };

            var entry = _dbContext.Attach(book).State = EntityState.Added;

            await _dbContext.SaveChangesAsync();
        }

        public async Task TestEF10()
        {
            var query = _dbSet
                .Where(q => Enumerable.Range(0, 10).Contains(q.Id))
                .Where(q => Enumerable.Range(5, 9).Contains(q.Id));
            // 0 == 1 
            _logger.LogDebug(query.ToQueryString());

            await query.ToListAsync();
        }

        public async Task TestEF11()
        {
            var query = _dbSet
                .Where(q => new string[] { "hello", "me" }.Contains(q.Name));
            _logger.LogDebug(query.ToQueryString());

            await query.ToListAsync();
        }

        public async Task TestEF12()
        {
            var query = _dbContext.Set<Stock>()
                .Where(q => new StockStatus[] { StockStatus.ReTurned, StockStatus.New }.Contains(q.Status));
            _logger.LogDebug(query.ToQueryString());

            await query.ToListAsync();
        }

        public async Task TestEF13()
        {
            var query = _dbSet
                .Where(q => new string[] { "hello", "me" }.Contains(q.Name));

            query = query.Intersect(_dbSet
                .Where(q => new string[] { "rew" }.Contains(q.Name)));

            _logger.LogDebug(query.ToQueryString());

            await query.ToListAsync();
        }

        public async Task TestEF14()
        {
            var permission = await _dbSet.FirstOrDefaultAsync();

            var t = _dbContext.Attach(new Permission
            {
                Id = permission.Id
            });
        }

        public async Task TestEF15()
        {

            var t = new Permission
            {
                Id = 23
            };

            var temp = _dbSet.Entry(t);



            await Task.CompletedTask;
        }

        public async Task TestEF16()
        {
            var tracker = _dbContext.ChangeTracker;

            var t = new Book
            {
                Id = 12, Title = "Tesing Ef", Pages = 12,
            };

            var temp = _dbContext.Set<Book>().Attach(t);

            t.Title = "dfsfsd";

            await _dbContext.SaveChangesAsync();

            await Task.CompletedTask;
        }

        public async Task TestEF17()
        {

            var temp = _dbContext.Set<Stock>().Select(x => new Stock
            {
                StockId = x.StockId,
                Book = x.Book,
                Repository = x.Repository,
                Reservation = x.Reservation
            }).ToList();

            await Task.CompletedTask;
        }

        public async Task TestEF18()
        {

            var temp = _dbContext.Set<RolePermission>();

            var entry = new RolePermission
            {
                RoleId = 1000,
                PermissionId = 1200,
            };
            _dbContext.Entry(entry).Property("Test1").CurrentValue = 2;

            var t = temp.Update(entry);

            await SaveChangesAsync();
        }

        public async Task TestEF19()
        {
            var theBook = await _dbContext.Set<Book>().FindAsync(12);

            theBook.Pages = 3211;

            await SaveChangesAsync();
        }

        public async Task TestEF20()
        {
            var publishedDate = new DateTime?[] { DateTime.Now, null };

            var queryable = _dbContext.Set<Book>().AsQueryable();

            queryable = queryable.Where(b => publishedDate.Contains(b.PublishedDate));

            _logger.LogDebug("@{queryable}", queryable);

            var books = await queryable.ToListAsync();

        }

    }


}
