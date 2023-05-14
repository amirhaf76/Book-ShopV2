using BookShop.Models.Dtos.UserAccountDtos;
using BookShop.Models.DtosExtension;
using BookShop.ModelsLayer.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.ModelsLayer.GetwayLayer.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RegisterController
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger, IUserAccountRepository userAccountRepository)
        {
            _logger = logger;
            _userAccountRepository = userAccountRepository;
        }

        [HttpPost("User")]
        public async Task<UserCreationResponse> CreateUserAsync([FromBody] UserCreationRequest userCreationRequest)
        {
            var userCreationResponse = await _userAccountRepository.CreateUserAsync(new UserCreationDto
            {
                Password = userCreationRequest.Password,
                Username = userCreationRequest.Username,
            });

            return userCreationResponse.ConvertToUserCreationResponse();
        }
    }
}
