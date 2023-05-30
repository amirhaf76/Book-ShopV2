using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.RequestResponseModels;

namespace BookShop.ModelsLayer.ViewModelLayer.VMExtension
{
    public static class BookVMExtension
    {
        public static BookUpdateDto ConvertToBookUpdateRequest(this BookUpdateRequest bookUpdateRequest)
        {
            return new BookUpdateDto
            {
                Id = bookUpdateRequest.Id,
                Title = bookUpdateRequest.Title,
                PageNumbers = bookUpdateRequest.PageNumbers,
                PublishedDate = bookUpdateRequest.PublishedDate,
                AuthorIds = bookUpdateRequest.AuthorIds.ToList(),
            };
        }

        public static BookCreationDto ConvertToBookCreationDto(this BookCreationRequest bookCreationRequest)
        {
            return new BookCreationDto
            {
                Title = bookCreationRequest.Title,
                PageNumbers = bookCreationRequest.PageNumbers,
                PublishedDate = bookCreationRequest.PublishedDate,
                AuthorIds = bookCreationRequest.AuthorIds?.ToList(),
            };
        }

        public static BookCreationResponse ConvertToBookCreationResponse(this BookCreationDto bookCreationDto)
        {
            return new BookCreationResponse
            {
                Id = bookCreationDto.Id ?? 0,
            };
        }

        public static BookUpdateResponse ConvertToBookUpdateResponse(this BookUpdateDto bookUpdateDto)
        {
            return new BookUpdateResponse
            {
                Id = bookUpdateDto.Id,
                Title = bookUpdateDto.Title,
                PageNumbers = bookUpdateDto.PageNumbers,
                PublishedDate = bookUpdateDto.PublishedDate,
                AuthorIds = bookUpdateDto.AuthorIds.ToList(),
            };
        }

        public static BookQueryResponse ConvertToBookQueryResponse(this BookQueryDto bookQueryDto)
        {
            return new BookQueryResponse
            {
                Id = bookQueryDto.Id,
                Title = bookQueryDto.Title,
                PageNumbers = bookQueryDto.PageNumbers,
                PublishedDate = bookQueryDto.PublishedDate,
                Authors = bookQueryDto.Authors,
            };
        }
    }
}
