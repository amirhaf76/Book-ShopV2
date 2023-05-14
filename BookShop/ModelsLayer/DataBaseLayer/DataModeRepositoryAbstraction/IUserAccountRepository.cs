using BookShop.ModelsLayer.Dtos.UserAccountDtos;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModeRepositoryAbstraction
{
    public interface IUserAccountRepository
    {
        Task<UserCreationResponseDto> CreateUserAsync(UserCreationDto userCreationDto);
    }
}