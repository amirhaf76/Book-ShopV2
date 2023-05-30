using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IBookService
    {
        Task<BookCreationDto> CreateBookAsync(BookCreationDto createBookRequest);

        Task<BookUpdateDto> UpdateBookAsync(BookUpdateDto bookUpdateDto);

        Task RemoveBookAsync(int id);
    }
}
