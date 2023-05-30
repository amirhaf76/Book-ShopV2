using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.AuthenticationDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.UserAccountDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IAuthenticationService
    {
        Task<TokenDto> Authenticate(UserAccountDto userAccount);
    }
}