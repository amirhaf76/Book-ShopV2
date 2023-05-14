using BookShop.ModelsLayer.Dtos.AuthorDtos;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction
{
    public interface IAuthorService
    {
        Task<AuthorDto> AddAuthorIfItDoesntExistAsync(AuthorDto author);
    }
}
