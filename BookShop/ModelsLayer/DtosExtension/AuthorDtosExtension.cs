using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.Dtos.AuthorDtos;

namespace BookShop.ModelsLayer.DtosExtension
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
