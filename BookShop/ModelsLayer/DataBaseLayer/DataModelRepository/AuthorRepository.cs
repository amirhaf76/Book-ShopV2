using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.DataBaseLayer.DataModeRepositoryAbstraction;
using BookShop.ModelsLayer.DataBaseLayer.DbContexts.BookShopDbContexts;
using BookShop.ModelsLayer.Exceptions;
using Infrastructure.AutoFac.FlagInterface;
using Microsoft.EntityFrameworkCore;

namespace BookShop.ModelsLayer.DataBaseLayer.DataModelRepository
{
    public class AuthorRepository : IAuthorRepository, IScope
    {
        private readonly DbContext _bookShopDbContext;
        private readonly ILogger<AuthorRepository> _logger;

        public AuthorRepository(ILogger<AuthorRepository> logger, IBookShopDbContext bookShopDbContext)
        {
            _logger = logger;
            _bookShopDbContext = bookShopDbContext.GetDbContext();
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

        public async Task<Author> AddAuthorIfItDoesntExistAsync(Author author)
        {
            var receivedAuthor = await FindAuthorOrDefaultAsync(author);

            if (receivedAuthor != default)
            {
                return receivedAuthor;
            }
            else
            {
                var addedAuthor = await _bookShopDbContext.Set<Author>().AddAsync(author);

                await _bookShopDbContext.SaveChangesAsync();

                return addedAuthor.Entity;
            }
        }

        public async Task<IEnumerable<Author>> FindAuthorsByIdAsync(IEnumerable<int> authorIds)
        {
            var receivedAuthors = new List<Author>();

            foreach (var id in authorIds.Distinct())
            {
                var receivedAuthor = await FindAuthorByIdAsync(id);

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
                return await FindAuthorByIdAsync(id);
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
            var queryable = _bookShopDbContext.Set<Author>().Where(q => q.FirstName == author.FirstName && q.LastName == author.LastName);

            _logger.LogDebug("Find author query: {query}", queryable.ToQueryString());

            return await GetAuthorAsync(queryable);
        }

        public async Task<Author> FindAuthorByIdAsync(int id)
        {
            var queryable = _bookShopDbContext.Set<Author>().Where(q => q.Id == id);

            _logger.LogDebug("Find author query: {query}", queryable.ToQueryString());

            return await GetAuthorAsync(queryable);
        }


        private static async Task<Author> GetAuthorAsync(IQueryable<Author> queryable)
        {
            var receviedAuthor = await queryable.FirstOrDefaultAsync();

            if (receviedAuthor == default)
            {
                throw new AuthorNotFoundException("There is no author like firstName: '{author.FirstName}', LastName: '{author.LastName}'!");
            }

            return receviedAuthor;
        }


    }
}
