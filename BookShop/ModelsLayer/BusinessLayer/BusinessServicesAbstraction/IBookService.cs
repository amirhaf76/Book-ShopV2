﻿using BookShop.ModelsLayer.Dtos.BookDtos;

namespace BookShop.ModelsLayer.BusinessLayer.BusinessServicesAbstraction
{
    public interface IBookService
    {
        Task<BookCreationDto> CreateBookAsync(BookCreationDto createBookRequest);
    }
}