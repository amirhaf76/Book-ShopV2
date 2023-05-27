using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.Dtos.BookDtos;

namespace BookShop.ModelsLayer.DtosExtension
{
    public static class BookDtosExtension
    {
        public static BookQueryDto ConvertToBookQueryDto(this Book book)
        {
            return new BookQueryDto
            {
                Id = book.Id,
                PageNumbers = book.Pages,
                Title = book.Title,
                PublishedDate = book.PublishedDate,
                Authors = book.Authors.Select(AuthorDtosExtension.ConvertToAuthorDto),
            };
        }

        public static BookUpdateDto ConvertToBookUpdateDto(this Book book)
        {
            return new BookUpdateDto
            {
                Id = book.Id,
                PageNumbers = book.Pages,
                Title = book.Title,
                PublishedDate = book.PublishedDate,
                AuthorIds = book.Authors.Select(x => x.Id),
            };
        }

        public static BookCreationDto ConvertToBookCreationDto(this Book book)
        {
            return new BookCreationDto
            {
                Id = book.Id,
                PageNumbers = book.Pages,
                Title = book.Title,
                PublishedDate = book.PublishedDate,
                AuthorIds = book.Authors.Select(x => x.Id),
            };
        }

        public static Book UpdateBook(this Book book, BookUpdateDto bookUpdateDto, ICollection<Author> authors)
        {
            book.Id = bookUpdateDto.Id;
            book.Pages = bookUpdateDto.PageNumbers;
            book.Title = bookUpdateDto.Title;
            book.PublishedDate = bookUpdateDto.PublishedDate;
            book.Authors = authors;

            return book;
        }

    }
}
