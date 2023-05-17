using BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Dtos.AuthorDtos;
using BookShop.ModelsLayer.DtosExtension;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServices
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
            var receivedAuthor = await _authorRepository.FindAuthorOrDefaultAsync(author.ConvertToAuthor());

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
