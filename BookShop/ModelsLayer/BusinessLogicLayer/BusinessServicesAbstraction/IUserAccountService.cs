using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.UserAccountDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IUserAccountService
    {
        Task<UserCreationResponseDto> CreateUserAsync(UserCreationDto userCreationDto);
    }
}
