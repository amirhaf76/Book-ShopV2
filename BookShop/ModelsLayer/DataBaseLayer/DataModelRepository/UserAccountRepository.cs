using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepository
{
    public class UserAccountRepository : BaseRepository<UserAccount>, IUserAccountRepository, IScope
    {
        private readonly ILogger<BookRepository> _logger;

        public UserAccountRepository(ILogger<BookRepository> logger, DbContext dbContext) : base(dbContext)
        {
            _logger = logger;
        }


        public async Task<UserAccount> GetUserAccountAsync(string username)
        {
            return await _dbSet
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();
        }
    }


}
