using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Dtos.AuthenticationDtos;
using BookShop.ModelsLayer.Dtos.UserAccountDtos;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServices
{
    public class AuthenticationService : IAuthenticationService, IScope
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IExceptionCaseService _exceptionCaseService;
        private readonly IPasswordHasher<UserAccount> _passwordHasher;


        public AuthenticationService(ILogger<AuthenticationService> logger, IConfiguration configuration, IUserAccountRepository userAccountRepository, IExceptionCaseService exceptionCaseService, IPasswordHasher<UserAccount> passwordHasher)
        {
            _logger = logger;
            _configuration = configuration;
            _userAccountRepository = userAccountRepository;
            _exceptionCaseService = exceptionCaseService;
            _passwordHasher = passwordHasher;
        }


        public async Task<TokenDto> Authenticate(UserAccountDto userAccount)
        {
            var foundUserAccount = await _userAccountRepository.GetUserAccountAsync(userAccount.Username);

            if (foundUserAccount == null)
            {
                throw _exceptionCaseService.CreateUsernameOrPasswordIsIncorrectException();
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(foundUserAccount, foundUserAccount.Password, userAccount.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw _exceptionCaseService.CreateUsernameOrPasswordIsIncorrectException();
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
