using BookShop.Models.BusinessServiceAbstraction;
using BookShop.Models.DataBaseLayer.DataBaseModels;
using BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.Models.DataBaseLayer.DbContexts.BookShopDbContexts;
using BookShop.Models.Dtos.UserAccountDtos;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Models.DataBaseLayer.DataModelRepository
{
    public class UserAccountRepository : IUserAccountRepository, IScope
    {
        private readonly ILogger<BookRepository> _logger;
        private readonly IExceptionCaseService _exceptionCaseService;
        private readonly IPasswordHasher<UserAccount> _passwordHasher;
        private readonly DbContext _dbContext;

        public UserAccountRepository(ILogger<BookRepository> logger, IBookShopDbContext bookShopDbContext, IPasswordHasher<UserAccount> passwordHasher, IExceptionCaseService exceptionCaseService)
        {
            _logger = logger;
            _dbContext = bookShopDbContext.GetDbContext();
            _passwordHasher = passwordHasher;
            _exceptionCaseService = exceptionCaseService;
        }

        public async Task<UserCreationResponseDto> CreateUserAsync(UserCreationDto userCreationDto)
        {
            userCreationDto.Username = userCreationDto.Username.Trim().ToLower();

            var user = await _dbContext
                .Set<UserAccount>()
                .Where(x => x.Username == userCreationDto.Username)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                throw _exceptionCaseService.UserAccountIsExistException(user.Username);
            }

            var userAccount = new UserAccount
            {
                Username = userCreationDto.Username,
                Password = userCreationDto.Password,
                RegeisteredDate = DateTime.UtcNow,
            };

            var hashedPassword = _passwordHasher.HashPassword(userAccount, userAccount.Password);

            var addedUserAccount = await _dbContext.Set<UserAccount>().AddAsync(new UserAccount
            {
                Username = userAccount.Username,
                Password = hashedPassword,
                RegeisteredDate = userAccount.RegeisteredDate,
            });

            await _dbContext.SaveChangesAsync();

            return new UserCreationResponseDto
            {
                Id = addedUserAccount.Entity.Id,
                UserName = addedUserAccount.Entity.Username,
                RegeisteredDate = addedUserAccount.Entity.RegeisteredDate,
            };
        }

    }
}
