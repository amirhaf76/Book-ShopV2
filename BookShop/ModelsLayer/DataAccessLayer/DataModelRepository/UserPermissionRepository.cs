using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepository
{
    public class UserPermissionRepository : BaseRepository<UserPermission>, IUserPermissionRepository, IScope
    {
        private readonly ILogger<UserPermissionRepository> _logger;

        public UserPermissionRepository(ILogger<UserPermissionRepository> logger, DbContext dbContexts) : base(dbContexts)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<UserPermission>> GetUserPermissionByIdAsync(int userId)
        {
            var gettingUserPermissionQuery = _dbSet
                .AsQueryable()
                .Where(q => q.UserId == userId)
                .Include(q => q.Permission)
                .AsSplitQuery();

            return await gettingUserPermissionQuery.ToListAsync();
        }
    }
}
