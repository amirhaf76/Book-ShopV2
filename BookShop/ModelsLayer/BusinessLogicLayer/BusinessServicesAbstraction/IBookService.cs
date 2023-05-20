using BookShop.ModelsLayer.Dtos.BookDtos;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction
{
    public interface IBookService
    {
        Task<BookCreationDto> CreateBookAsync(BookCreationDto createBookRequest);

        Task<BookUpdateDto> UpdateBookAsync(BookUpdateDto bookUpdateDto);

        Task RemoveBookAsync(int id);
    }
}
