using Autofac;
using Autofac.Extras.Moq;
using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices;
using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
using BookShop.ModelsLayer.Exceptions;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.MockRepositoryPattern;
using BookShop.Test.UnitTest.Core.Scenarios;
using BookShop.Test.UnitTest.Core.Scenarios.CollectionAndTestCaseOrders;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios.ServicesScenarios
{
    [Order((int)ServiceTestCollectionScenarioOrder.RepositoryServiceTestCases)]
    [Collection(nameof(CollectionTestOrder.Service))]
    public class RepositoryServiceTestCases : BaseTestCaseScenario
    {
        private readonly ILogger<RepositoryServiceTestCases> _logger;

        private int _primaryKey = 4;
        private readonly Dictionary<int, Repository> _repositoryTable = new Dictionary<int, Repository>
        {
            { 1,  new Repository { Id = 1, Name = "Repository_1.1", IsEnable = true } },
            { 2,  new Repository { Id = 2, Name = "Repository_1.2", IsEnable = false } },
            { 3,  new Repository { Id = 3, Name = "Repository_1.3", IsEnable = true } },
        };

        public RepositoryServiceTestCases(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
            _logger = ResolveService<ILogger<RepositoryServiceTestCases>>();
        }

        [Fact]
        public async Task GetRepositories_WithoutFilter_AllRepositoriesMustBeReceived()
        {
            // Arrangement.
            var repositoryTable = new List<Repository>
            {
                new Repository { Id = 1, Name = "Repository_1.1", IsEnable = true },
                new Repository { Id = 2, Name = "Repository_1.2", IsEnable = false },
                new Repository { Id = 3, Name = "Repository_1.3", IsEnable = true },
            };

            using var mock = AutoMock.GetStrict(builder =>
            {
                var repositoryMock = new Mock<IRepoRepository>();

                repositoryMock
                    .Setup(x => x.GetRepositoriesAsync(It.IsAny<RepositoryFilter>()))
                    .ReturnsAsync(() => repositoryTable);

                builder.RegisterMock(repositoryMock);

                builder
                    .RegisterType<RepositoryService>()
                    .As<IRepositoryService>();
            });

            var repositoryService = mock.Create<IRepositoryService>();

            // Action.
            var gettingRepositoriesResult = await repositoryService.GetRepositoriesAsync();

            // Assertion.
            gettingRepositoriesResult.Should().BeEquivalentTo(repositoryTable.Select(x => new
            {
                x.Id,
                x.Name,
                x.IsEnable,
                x.AddressId
            }));
        }

        [Fact]
        public async Task GetRepositories_ByAnAvailableRepositoryId_TheRepositoryMustBeReceivedSuccessfully()
        {
            // Arrangement
            const int AVAILABLE_ID = 1;

            var repositoryTable = new List<Repository>
            {
                new Repository { Id = AVAILABLE_ID, Name = "Repository_1.1", IsEnable = true },
                new Repository { Id = 2, Name = "Repository_1.2", IsEnable = true },
            };

            using var mock = AutoMock.GetStrict(builder =>
            {
                var repositoryMock = new Mock<IRepoRepository>();

                repositoryMock
                    .Setup(x => x.GetRepositoriesAsync(It.IsAny<RepositoryFilter>()))
                    .ReturnsAsync((RepositoryFilter filter) => repositoryTable.Where(x => x.Id == filter.Id ));

                builder.RegisterMock(repositoryMock);

                builder
                    .RegisterType<RepositoryService>()
                    .As<IRepositoryService>();
            });

            var repositoryService = mock.Create<IRepositoryService>();

            var repositoryFilter = new GettingRepositoriesFilter
            {
                Id = AVAILABLE_ID,
            };

            // Action.
            var gettingRepositoriesResult = await repositoryService.GetRepositoriesAsync(repositoryFilter);

            // Assertion.
            gettingRepositoriesResult.Should().BeEquivalentTo(new []
            {
                new RepositoryResult { Id = 1, Name = "Repository_1.1", IsEnable = true, AddressId = null, }
            });
        }

        [Fact]
        public async Task GetRepositories_ByAnUnavailableRepositoryId_AnEmptyCollectionMustBeReceived()
        {
            // Arrangement
            const int UNAVAILABLE_ID = 50;

            var repositoryTable = new List<Repository>
            {
                new Repository { Id = 1, Name = "Repository_1.1", IsEnable = true },
                new Repository { Id = 2, Name = "Repository_1.2", IsEnable = true },
            };

            using var mock = AutoMock.GetStrict(builder =>
            {
                var repositoryMock = new Mock<IRepoRepository>();

                repositoryMock
                    .Setup(x => x.GetRepositoriesAsync(It.IsAny<RepositoryFilter>()))
                    .ReturnsAsync((RepositoryFilter filter) => repositoryTable.Where(x => x.Id == filter.Id));

                builder.RegisterMock(repositoryMock);

                builder
                    .RegisterType<RepositoryService>()
                    .As<IRepositoryService>();
            });

            var repositoryService = mock.Create<IRepositoryService>();

            var repositoryFilter = new GettingRepositoriesFilter
            {
                Id = UNAVAILABLE_ID,
            };

            // Action.
            var gettingRepositoriesResult = await repositoryService.GetRepositoriesAsync(repositoryFilter);

            // Assertion.
            gettingRepositoriesResult.Should().BeEmpty();
        }

        [Fact]
        public async Task AddRepository_NormalInformationRepository_TheRepositoryMustBeAddedSuccessfully()
        {
            // Arrangement
            const int NEW_ID = 3;

            var repositoryTable = new List<Repository>
            {
                new Repository { Id = 1, Name = "Repository_1.1", IsEnable = true },
                new Repository { Id = 2, Name = "Repository_1.2", IsEnable = false },
            };

            var currentTableCount = repositoryTable.Count;

            using var mock = AutoMock.GetStrict(builder =>
            {
                var repositoryMock = new Mock<IRepoRepository>();

                repositoryMock
                    .Setup(x => x.AddAsync(It.IsAny<Repository>()))
                    .Callback((Repository repository) =>
                    {
                        repository.Id = NEW_ID;

                        repositoryTable.Add(repository);
                    })
                    .ReturnsAsync((Repository repository) => repository);

                builder.RegisterMock(repositoryMock);

                builder
                    .RegisterType<RepositoryService>()
                    .As<IRepositoryService>();
            });

            var repositoryService = mock.Create<IRepositoryService>();

            var aRepository = new RecordingRepositoryDto
            {
                Name = "Repository_1.2",
                IsEnable = true,
            };

            // Action.
            var gettingRepositoriesResult = await repositoryService.AddRepositoryAsync(aRepository);

            // Assertion.
            (repositoryTable.Count - currentTableCount).Should().Be(1);

            gettingRepositoriesResult.Should().BeEquivalentTo(new
            {
                aRepository.Name,
                aRepository.IsEnable,
            });

        }


        [Fact]
        public async Task RemoveRepository_ARecordedRepository_TheRepositoryMustBeRemovedSuccessfully()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aRepository = MakeRemovalRepositoryDtoForAvailableRepository();

            var repositoryRemovalAction = async () => await repositoryService.UpdateRepositoryAsync(aRepository);

            var repositoryRemovalResult = await repositoryRemovalAction.Should().NotThrowAsync();

            repositoryRemovalResult.Subject.Should().BeEquivalentTo(GetTheRepository());
        }

        [Fact]
        public async Task RemoveRepository_AnUnrecordedRepository_AnRelatedExceptionMustBeThrow()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aRepository = MakeRemovalRepositoryDtoForUnavailableRepository();

            var repositoryRemovalAction = async () => await repositoryService.UpdateRepositoryAsync(aRepository);

            var repositoryRemovalResult = await repositoryRemovalAction.Should().ThrowAsync<RepositoryNotFoundException>();
        }


        private ContainerBuilder RegisterRepositoryServiceAndItsDependencies(ContainerBuilder builder)
        {
            var mockRepository = new MockRepositoryBuilder<int, Repository>(x => x.Id, (k, r) => r.Id = k, () => _primaryKey++, _repositoryTable)
                .CreateMockBaseRepository<IRepoRepository>();

            builder.RegisterMock(mockRepository);

            builder
                .RegisterType<RepositoryService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();



            return builder;
        }


        private static UpdateRespositoryDto MakeRemovalRepositoryDtoForAvailableRepository()
        {
            return new UpdateRespositoryDto
            {

            };
        }

        private static UpdateRespositoryDto MakeRemovalRepositoryDtoForUnavailableRepository()
        {
            return new UpdateRespositoryDto
            {

            };
        }


 


        private object GetTheRepository()
        {
            var repository = _repositoryTable[2];

            return new
            {
                repository.Name,
                repository.IsEnable,
            };
        }

    }
}