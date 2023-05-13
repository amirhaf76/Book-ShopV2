using BookShop.Models.Dtos.UserAccountDtos;

namespace BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction
{
    public interface IUserAccountRepository
    {
        Task<UserCreationResponseDto> CreateUserAsync(UserCreationDto userCreationDto);
    }
}