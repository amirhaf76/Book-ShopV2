using BookShop.ModelsLayer.Dtos.AuthenticationDtos;
using BookShop.ModelsLayer.Dtos.UserAccountDtos;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModeRepositoryAbstraction
{
    public interface IAuthenticationRepository
    {
        Task<TokenDto> Authenticate(UserAccountDto userAccount);
    }
}