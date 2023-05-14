using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Dtos.UserAccountDtos;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.AspNetCore.Identity;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServices
{
    public class UserAccountService : IUserAccountService, IScope
    {
        private readonly ILogger<UserAccountService> _logger;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IExceptionCaseService _exceptionCaseService;
        private readonly IPasswordHasher<UserAccount> _passwordHasher;

        public UserAccountService(ILogger<UserAccountService> logger, IUserAccountRepository userAccountRepository, IExceptionCaseService exceptionCaseService, IPasswordHasher<UserAccount> passwordHasher)
        {
            _logger = logger;
            _userAccountRepository = userAccountRepository;
            _exceptionCaseService = exceptionCaseService;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserCreationResponseDto> CreateUserAsync(UserCreationDto userCreationDto)
        {
            userCreationDto.Username = userCreationDto.Username.Trim().ToLower();

            var user = await _userAccountRepository.GetUserAccountAsync(userCreationDto.Username);

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

            var addedUserAccount = await _userAccountRepository.AddAsync(new UserAccount
            {
                Username = userAccount.Username,
                Password = hashedPassword,
                RegeisteredDate = userAccount.RegeisteredDate,
            });

            await _userAccountRepository.SaveChangesAsync();

            return new UserCreationResponseDto
            {
                Id = addedUserAccount.Entity.Id,
                UserName = addedUserAccount.Entity.Username,
                RegeisteredDate = addedUserAccount.Entity.RegeisteredDate,
            };
        }
    }
}
