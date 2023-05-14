using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DbContexts.BookShopDbContexts;
using BookShop.ModelsLayer.Dtos.UserAccountDtos;
using BookShop.ModelsLayer.GetwayLayer.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("Token")]
        public async Task<TokenResponse> GetTokenAsync([FromBody] TokenRequest tokenRequest)
        {
            var userAccountDto = new UserAccountDto
            {
                Username = tokenRequest.Username,
                Password = tokenRequest.Password,
            };

            var generatedToken = await _authenticationService.Authenticate(userAccountDto);

            return new TokenResponse
            {
                Token = generatedToken.Token,
            };
        }


    }
}
