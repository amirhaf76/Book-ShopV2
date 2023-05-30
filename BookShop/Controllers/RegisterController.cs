using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.UserAccountDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RegisterController
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IUserAccountService _userAccountService;

        public RegisterController(ILogger<RegisterController> logger, IUserAccountService userAccountService)
        {
            _logger = logger;
            _userAccountService = userAccountService;
        }

        [HttpPost("User")]
        public async Task<UserCreationResponse> CreateUserAsync([FromBody] UserCreationRequest userCreationRequest)
        {
            var userCreationResponse = await _userAccountService.CreateUserAsync(new UserCreationDto
            {
                Password = userCreationRequest.Password,
                Username = userCreationRequest.Username,
            });

            return userCreationResponse.ConvertToUserCreationResponse();
        }
    }
}
