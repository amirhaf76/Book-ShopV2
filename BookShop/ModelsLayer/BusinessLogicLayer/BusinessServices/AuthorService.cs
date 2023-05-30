using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.AuthorDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class AuthorService : IAuthorService, IScope
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorDto> AddAuthorIfItDoesntExistAsync(AuthorDto author)
        {
            var receivedAuthor = await _authorRepository.FindAuthorOrDefaultAsync(author.Id);

            if (receivedAuthor != default)
            {
                return receivedAuthor.ConvertToAuthorDto();
            }
            else
            {
                var addedAuthor = await _authorRepository.AddAsync(author.ConvertToAuthor());

                await _authorRepository.SaveChangesAsync();

                return addedAuthor.ConvertToAuthorDto();
            }
        }
    }
}
