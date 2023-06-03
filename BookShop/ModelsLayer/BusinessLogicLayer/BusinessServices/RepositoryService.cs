using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.Exceptions;
using Infrastructure.AutoFac.FlagInterface;

namespace BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices
{
    public class RepositoryService : IRepositoryService, IScope
    {
        private readonly IRepoRepository _repoRepository;

        public RepositoryService(IRepoRepository repoRepository)
        {
            _repoRepository = repoRepository;
        }

        public async Task<RecordingRepositoryResultDto> AddRepositoryAsync(RecordingRepositoryDto aRepository)
        {
            var newRepository = new Repository
            {
                IsEnable = aRepository.IsEnable,
                Name = aRepository.Name
            };

            var addedRepository = await _repoRepository.AddAsync(newRepository);

            await _repoRepository.SaveChangesAsync();

            return new RecordingRepositoryResultDto
            {
                Id = addedRepository.Id,
                Name = addedRepository.Name,
                IsEnable = addedRepository.IsEnable,
            };
        }

        public async Task<IEnumerable<RepositoryResult>> GetRepositoriesAsync(GettingRepositoriesFilter filter)
        {
            var receivedStocks = await _repoRepository.GetRepositoriesAsync(filter.ConvertToRepositoryFilter());

            return receivedStocks.Select(r => r.ConvertToRepositoryResult());
        }

        public async Task<IEnumerable<RepositoryResult>> GetRepositoriesAsync()
        {
            return await GetRepositoriesAsync(new GettingRepositoriesFilter());
        }

        public async Task<UpdateRespositoryResultDto> UpdateRepositoryAsync(UpdateRespositoryDto updateRespository)
        {
            var theRepository = await _repoRepository.FindAsync(updateRespository.Id);

            if (theRepository == null)
            {
                throw new RepositoryNotFoundException(updateRespository.Id);
            }

            theRepository.Name = updateRespository.Name;
            theRepository.AddressId = updateRespository.AddressId;
            theRepository.IsEnable = updateRespository.IsEnable;

            await _repoRepository.SaveChangesAsync();

            return theRepository.ConvertToUpdateRespositoryResultDto();
        }

    }
}
