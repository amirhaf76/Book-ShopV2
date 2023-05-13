using BookShop.Models.DataBaseLayer.DataBaseModels;
using BookShop.Models.Dtos;

namespace BookShop.Models.DtosExtension
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
    }
}
