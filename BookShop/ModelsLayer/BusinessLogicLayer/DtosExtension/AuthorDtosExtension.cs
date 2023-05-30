using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.AuthorDtos;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;

namespace BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension
{
    public static class AuthorDtosExtension
    {
        public static AuthorDto ConvertToAuthorDto(this Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
            };
        }

        public static Author ConvertToAuthor(this AuthorDto author)
        {
            return new Author
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
            };
        }
    }
}
