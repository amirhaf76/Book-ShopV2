using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.AuthorDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IAuthorService
    {
        Task<AuthorDto> AddAuthorIfItDoesntExistAsync(AuthorDto author);
    }
}
