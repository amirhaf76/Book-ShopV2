using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction
{
    public interface IRepositoryService
    {
        Task<IEnumerable<RepositoryResult>> GetRepositoriesAsync(GettingRepositoriesFilter filter);

        Task<IEnumerable<RepositoryResult>> GetRepositoriesAsync();

        Task<RecordingRepositoryResultDto> AddRepositoryAsync(RecordingRepositoryDto aRepository);

        Task<UpdateRespositoryResultDto> UpdateRepositoryAsync(UpdateRespositoryDto aRepository);
    }
}
