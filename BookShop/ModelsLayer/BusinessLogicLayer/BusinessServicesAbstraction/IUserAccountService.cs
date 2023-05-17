using BookShop.ModelsLayer.Dtos.UserAccountDtos;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction
{
    public interface IUserAccountService
    {
        Task<UserCreationResponseDto> CreateUserAsync(UserCreationDto userCreationDto);
    }
}
