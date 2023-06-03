using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.RequestResponseModels;

namespace BookShop.ModelsLayer.ViewModelLayer.VMExtension
{
    public static class RepositoryVMExtension
    {
        public static GettingRepositoriesFilter ConvertToGettingRepositoriesFilter(this GettingRepositoriesFilterVM filter)
        {
            return new GettingRepositoriesFilter
            {
                AddressId = filter.AddressId,
                Id = filter.Id,
                IsEnable = filter.IsEnable,
                Name = filter.Name,
                Pagination = new PaginationFilterDto
                {
                    PageNo = filter.PageNumber,
                    PageSiz = filter.PageSize,
                },
            };
        }

        public static RepositoryVM ConvertToRepositoryVM(this RepositoryResult repository)
        {
            return new RepositoryVM
            {
                Id = repository.Id,
                AddressId = repository.AddressId,
                IsEnable = repository.IsEnable,
                Name = repository.Name,
            };
        }

        public static RecordingRepositoryDto ConvertToRecordingRepositoryDto(this RecordingRepositoryVM repository)
        {
            return new RecordingRepositoryDto
            {
                Name = repository.Name,
                IsEnable =repository.IsEnable,
            };
        }

        public static RecordingRepositoryResultVM ConvertToRecordingRepositoryVM(this RecordingRepositoryResultDto repository)
        {
            return new RecordingRepositoryResultVM
            {
                Id = repository.Id,
                Name = repository.Name,
                IsEnable = repository.IsEnable,
            };
        }
        public static UpdateRespositoryDto ConvertToRemovalRespositoryDto(this UpdateRespositoryVM repository)
        {
            return new UpdateRespositoryDto
            {
                Id = repository.Id,
                IsEnable = repository.IsEnable,
                AddressId = repository.AddressId,
                Name = repository.Name,
            };
        }
        public static UpdateRespositoryResultVM ConvertToUpdateRespositoryVM(this UpdateRespositoryResultDto repository)
        {
            return new UpdateRespositoryResultVM
            {
                Id = repository.Id,
                IsEnable = repository.IsEnable,
                Name = repository.Name,
                AddressId = repository.AddressId,
            };
        }


    }
}
