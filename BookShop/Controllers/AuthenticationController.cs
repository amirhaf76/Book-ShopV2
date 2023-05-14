using BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.Models.DataBaseLayer.DbContexts.BookShopDbContexts;
using BookShop.Models.Dtos.UserAccountDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly DbContext _dbContext;
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(ILogger<AuthenticationController> logger, IBookShopDbContext bookShopDbContext, IAuthenticationRepository authenticationRepository)
        {
            _logger = logger;
            _dbContext = bookShopDbContext.GetDbContext();
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost("Token")]
        public async Task<TokenResponse> GetTokenAsync([FromBody] TokenRequest tokenRequest)
        {
            var userAccountDto = new UserAccountDto
            {
                Username = tokenRequest.Username,
                Password = tokenRequest.Password,
            };

            var generatedToken = await _authenticationRepository.Authenticate(userAccountDto);

            return new TokenResponse
            {
                Token = generatedToken.Token,
            };
        }


    }
}
