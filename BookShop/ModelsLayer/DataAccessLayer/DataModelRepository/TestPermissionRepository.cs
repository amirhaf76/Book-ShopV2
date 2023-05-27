using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepository;
using BookShop.ModelsLayer.DataBaseLayer.DbContexts.BookShopDbContexts;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class TestPermissionRepository : BaseRepository<Permission>, ITestPermissionRepository, IScope
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
            _dbContext = new BookShopDbContext(new DbContextOptionsBuilder().UseSqlServer(configuration.GetConnectionString("BookShopDB") ?? throw new InvalidOperationException("Connection string 'BookShopDB' not found.")).Options);

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

        public async Task TestEF()
        {
            var data = await _dbContext.AddAsync(new Permission() { Id = 7, Name = "Permission_2" });
            _logger.LogDebug("{@data}", data.Entity);

            _dbContext.SaveChanges();
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
    }


}
