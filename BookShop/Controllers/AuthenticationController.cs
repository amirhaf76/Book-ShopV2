using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.UserAccountDtos;
using BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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
