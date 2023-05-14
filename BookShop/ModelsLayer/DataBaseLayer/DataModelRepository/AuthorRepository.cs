using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Exceptions;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepository
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository, IScope
    {
        private readonly ILogger<AuthorRepository> _logger;

        public AuthorRepository(ILogger<AuthorRepository> logger, DbContext dbContext) : base(dbContext)
        {
            _logger = logger;
        }

        private async Task<IEnumerable<Author>> GetOrAddAuthorToDbContextAsync(IEnumerable<int> authorIds)
        {
            var authors = new List<Author>();

            foreach (var aAutorId in authorIds)
            {
                var existedAuthor = await FindAuthorByIdOrDefaultAsync(aAutorId);

                if (existedAuthor == default)
                {
                    throw new Exception("Author with 'Id: {authorRequest.Id}' does not exist!");
                }

                authors.Add(existedAuthor);
            }

            return authors;
        }



        public async Task<IEnumerable<Author>> FindAuthorsByIdAsync(IEnumerable<int> authorIds)
        {
            var receivedAuthors = new List<Author>();

            foreach (var id in authorIds.Distinct())
            {
                var receivedAuthor = await FindAuthorAsync(id);

                if (receivedAuthor != default)
                {
                    receivedAuthors.Add(receivedAuthor);
                }
            }

            return receivedAuthors;
        }


        public async Task<Author> FindAuthorByIdOrDefaultAsync(int id)
        {
            try
            {
                return await FindAuthorAsync(id);
            }
            catch (AuthorNotFoundException)
            {
                return default;
            }
        }

        public async Task<Author> FindAuthorOrDefaultAsync(Author author)
        {
            try
            {
                return await FindAuthorAsync(author);
            }
            catch (AuthorNotFoundException)
            {
                return default;
            }
        }

        public async Task<Author> FindAuthorAsync(Author author)
        {
            var receviedAuthor = await _dbSet
                .Where(q => q.FirstName == author.FirstName && q.LastName == author.LastName)
                .FirstOrDefaultAsync();

            if (receviedAuthor == default)
            {
                throw new AuthorNotFoundException("There is no author like firstName!");
            }

            return receviedAuthor;

        }

        public async Task<Author> FindAuthorAsync(string firstName, string lastName)
        {
            var receviedAuthor = await _dbSet
                .Where(q => q.FirstName == firstName && q.LastName == lastName)
                .FirstOrDefaultAsync();

            if (receviedAuthor == default)
            {
                throw new AuthorNotFoundException("There is no author like firstName!");
            }

            return receviedAuthor;
        }

        public async Task<Author> FindAuthorAsync(int id)
        {
            var receviedAuthor = await _dbSet
                .Where(q => q.Id == id)
                .FirstOrDefaultAsync();

            if (receviedAuthor == default)
            {
                throw new AuthorNotFoundException("There is no author like firstName!");
            }

            return receviedAuthor;
        }



    }
}
