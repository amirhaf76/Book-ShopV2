﻿using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;
using BookShop.ModelsLayer.Dtos.BookDtos;
using BookShop.ModelsLayer.GetwayLayer.RequestResponseModels;

namespace BookShop.ModelsLayer.DtosExtension
{
    public static class BookDtosExtension
    {
        public static BookCreationDto ConvertToBookCreationDto(this BookCreationRequest bookCreationRequest)
        {
            return new BookCreationDto
            {
                Title = bookCreationRequest.Title,
                PageNumbers = bookCreationRequest.PageNumbers,
                AuthorIds = bookCreationRequest.AuthorIds.ToList(),
            };
        }

        public static BookCreationResponse ConvertToBookCreationResponse(this BookCreationDto bookCreationDto)
        {
            return new BookCreationResponse
            {
                Id = bookCreationDto.Id,
            };
        }

        public static BookQueryResponse ConvertToBookQueryResponse(this BookQueryDto bookQueryDto)
        {
            return new BookQueryResponse
            {
                Id = bookQueryDto.Id,
                Title = bookQueryDto.Title,
                PageNumbers = bookQueryDto.PageNumbers,
                Authors = bookQueryDto.Authors,
            };
        }

        public static BookQueryDto ConvertToBookCreationDto(this Book book)
        {
            return new BookQueryDto
            {
                Id = book.Id,
                PageNumbers = book.Pages,
                Title = book.Title,
                Authors = book.Authors.Select(AuthorDtosExtension.ConvertToAuthorDto),
            };
        }
    }
}