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


        public async Task<Author> FindAuthorOrDefaultAsync(int id)
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
            var receviedAuthor = await _dbSet.FindAsync(id);

            if (receviedAuthor == default)
            {
                throw new AuthorNotFoundException("There is no author like firstName!");
            }

            return receviedAuthor;
        }



    }
}
