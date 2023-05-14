using BookShop.Models.Dtos;
using BookShop.Models.Dtos.UserAccountDtos;

namespace BookShop.Models.DataBaseLayer.DataModeRepositoryAbstraction
{
    public interface IAuthenticationRepository
    {
        Task<TokenDto> Authenticate(UserAccountDto userAccount);
    }
}