﻿using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension
{
    public static class RepositoryDtoExtension
    {
        public static RepositoryFilter ConvertToRepositoryFilter(this GettingStockBookFilter filter)
        {
            return new RepositoryFilter
            {
                AddressId = filter.AddressId,
                Id = filter.Id,
                IsEnable = filter.IsEnable,
                Name = filter.Name,
                Pagination = filter.Pagination?.ConvertToPaginationFilter(),
            };
        }

        public static RepositoryFilter ConvertToRepositoryFilter(this GettingRepositoriesFilter filter)
        {
            return new RepositoryFilter
            {
                AddressId = filter.AddressId,
                Id = filter.Id,
                IsEnable = filter.IsEnable,
                Name = filter.Name,
                Pagination = filter.Pagination?.ConvertToPaginationFilter(),
            };
        }

        public static IEnumerable<StockBookResultDto> ConvertToStockBookResultDtos(this Repository repository)
        {
            return repository.Stocks.Select(s => new StockBookResultDto
            {
                Book = s.Book.ConvertToStockedBookResult(),
                Status = s.Status.ConvertToStockStatusVariety(),
                StockId = s.StockId,
                Repository = repository.ConvertToRespositoryMinResult(),
            });
        }

        public static RepositoryResult ConvertToRepositoryResult(this Repository repository)
        {
            return  new RepositoryResult
            {
                Id = repository.Id,
                Name = repository.Name,
                IsEnable = repository.IsEnable,
                AddressId = repository.AddressId,
            };
        }

        
        public static RemovalRespositoryResultDto ConvertToRemovalRespositoryResultDto(this Repository repository)
        {
            return new RemovalRespositoryResultDto
            {
                Id = repository.Id,
                IsEnable = repository.IsEnable,
            };
        }

        public static RespositoryMinResult ConvertToRespositoryMinResult(this Repository repository)
        {
            return new RespositoryMinResult
            {
                Id = repository.Id,
                IsEnable = repository.IsEnable,
                Name = repository.Name,
            };
        }
    }
}
