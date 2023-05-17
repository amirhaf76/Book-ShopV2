using BookShop.ModelsLayer.Dtos.AuthenticationDtos;
using BookShop.ModelsLayer.Dtos.UserAccountDtos;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction
{
    public interface IAuthenticationService
    {
        Task<TokenDto> Authenticate(UserAccountDto userAccount);
    }
}