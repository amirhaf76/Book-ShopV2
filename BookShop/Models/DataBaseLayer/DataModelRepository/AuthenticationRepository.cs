using BookShop.Models.BusinessServiceAbstraction;
using BookShop.Models.DataBaseLayer.DataBaseModels;
using BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.Models.DataBaseLayer.DbContexts.BookShopDbContexts;
using BookShop.Models.Dtos;
using BookShop.Models.Dtos.UserAccountDtos;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookShop.Models.DataBaseLayer.DataModelRepository
{
    public class AuthenticationRepository : IAuthenticationRepository, IScope
    {
        private readonly DbContext _bookShopDbContext;
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IExceptionCaseService _exceptionCaseService;
        private readonly IConfiguration _configuration;

        private readonly IPasswordHasher<UserAccount> _passwordHasher;


        public AuthenticationRepository(ILogger<AuthorRepository> logger, IBookShopDbContext bookShopDbContext, IConfiguration configuration, IExceptionCaseService exceptionCaseService, IPasswordHasher<UserAccount> passwordHasher)
        {
            _logger = logger;
            _bookShopDbContext = bookShopDbContext.GetDbContext();
            _configuration = configuration;
            _exceptionCaseService = exceptionCaseService;
            _passwordHasher = passwordHasher;
        }

        public async Task<TokenDto> Authenticate(UserAccountDto userAccount)
        {
            var foundUserAccount = await _bookShopDbContext
                .Set<UserAccount>()
                .Where(x => x.Username == userAccount.Username)
                .FirstOrDefaultAsync();

            if (foundUserAccount == null)
            {
                throw _exceptionCaseService.UsernameOrPasswordIsIncorrectException();
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(foundUserAccount, foundUserAccount.Password, userAccount.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw _exceptionCaseService.UsernameOrPasswordIsIncorrectException();
            }
            
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userAccount.Username),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDto
            {
                Token = tokenHandler.WriteToken(token),
            };
        }
    }
}
