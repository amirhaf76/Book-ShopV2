using Autofac;
using Autofac.Extras.Moq;
using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos;
using BookShop.ModelsLayer.Exceptions;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios
{
    [Order((int)ScenariosOrder.RepositoryServiceTestCases)]
    public class RepositoryServiceTestCases : BaseTestCaseScenario
    {
        private readonly ILogger<RepositoryServiceTestCases> _logger;

        public RepositoryServiceTestCases(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
            _logger = ResolveService<ILogger<RepositoryServiceTestCases>>();
        }

        [Fact]
        public async Task StockBook_AnUnRecordedInstanceBook_TheBookMustBeAddedSuccessfully()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aBook = MakeStockingDtoForUnavailableBook();

            var additionAction = async () => await repositoryService.StockBookAsync(aBook);

            var additionResult = await additionAction.Should().NotThrowAsync();

            additionResult.Subject.StockId.Should().NotBe(default);
        }

        [Fact]
        public async Task StockBook_AnRecordedInstanceBook_MustNotBeAddedNewRecordAndSameCountAsBefore()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aBook = MakeStockingDtoForAvailableBook();

            var stockedBookId = GetStockIdOfAvailableBook();

            var additionAction = async () => await repositoryService.StockBookAsync(aBook);

            var additionResult = await additionAction.Should().NotThrowAsync();

            additionResult.Subject.StockId.Should().Be(stockedBookId);
        }

        [Fact]
        public async Task RemoveBook_AnRecordedInstanceBook_TheBookMustBeRemovedSuccessfully()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aBook = MakeReductionDtoForAvailableBooks();

            var reductionAction = async () => await repositoryService.ReduceBookAsync(aBook);

            var reductionResult = await reductionAction.Should().NotThrowAsync();

            reductionResult.Subject.ReducedQuantity.Should().BeEquivalentTo(aBook.StockIds);
        }

        [Fact]
        public async Task RemoveBook_AnUnRecordedInstanceBook_AnRelatedExceptionMustBeThrow()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aBook = MakeReductionDtoForAvailableBooksAndAnUnavailableBook();

            var reductionAction = async () => await repositoryService.ReduceBookAsync(aBook);

            var reductionResult = await reductionAction.Should().ThrowAsync<StockedBookNotFoundException>();
        }

        [Fact]
        public async Task GetBooks_WithoutFilter_AllBooksMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync();

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEquivalentTo(GetAllMockStockBook());
        }

        [Fact]
        public async Task GetBooks_ByAnAvailableBookId_TheBookWithAllItsInstanceMustBeReceivedSuccessfully()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync(MakeFilterDtoForStockedBookBaseOnBookId());

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEquivalentTo(GetAllStockBooksOfSpecificBook());
        }

        [Fact]
        public async Task GetBooks_ByAnUnavailableBookId_AnEmptyCollectionMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync(MakeFilterDtoForStockedBookBaseOnUnavailableBookId());

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBooks_ByAnAvailableStockedBookId_TheStockedBookMustBeReceivedSuccessfull()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync(MakeFilterDtoForStockedBookBaseOnStockBookId());

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEquivalentTo(GetTheStockBooks());
        }

        [Fact]
        public async Task GetBooks_ByAnUnavailableStockedBookId_AnEmptyCollectionMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync(MakeFilterDtoForStockedBookBaseOnUnavailableStockBookId());

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEmpty();
        }

        [Fact]
        public async Task GetRepositories_WithoutFilter_AllRepositoriesMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var gettingRepositoriesAction = async () => await repositoryService.GetRepositoriesAsync();

            var gettingRepositoriesResult = await gettingRepositoriesAction.Should().NotThrowAsync();

            gettingRepositoriesResult.Subject.Repositories.Should().BeEquivalentTo(GetTheRepositories());
        }

        [Fact]
        public async Task GetRepositories_ByAnAvailableRepositoryId_TheRepositoryMustBeReceivedSuccessfully()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var gettingRepositoriesAction = async () => await repositoryService.GetRepositoriesAsync(MakeFilterDtoForGettingRepositoriesBaseOnRepositoryId());

            var gettingRepositoriesResult = await gettingRepositoriesAction.Should().NotThrowAsync();

            gettingRepositoriesResult.Subject.Repositories.Should().BeEquivalentTo(GetTheRepositories());
        }

        [Fact]
        public async Task GetRepositories_ByAnUnavailableRepositoryId_AnEmptyCollectionMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var gettingRepositoriesAction = async () => await repositoryService.GetRepositoriesAsync(MakeFilterDtoForGettingRepositoriesBaseOnUnavailableRepositoryId());

            var gettingRepositoriesResult = await gettingRepositoriesAction.Should().NotThrowAsync();

            gettingRepositoriesResult.Subject.Repositories.Should().BeEmpty();
        }

        [Fact]
        public async Task AddRepository_AnUnRecordedRepository_TheRepositoryMustBeAddedSuccessfully()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aRepository = MakeRecordingRepositoryDtoForUnavailableRepository();

            var repositoryAdditionAction = async () => await repositoryService.AddRepositoryAsync(aRepository);

            var repositoryAdditionResult = await repositoryAdditionAction.Should().NotThrowAsync();

            repositoryAdditionResult.Subject.RepositoryId.Should().NotBe(default);
        }

        [Fact]
        public async Task AddRepository_ARecordedRepository_JustReturnRecordedRepositoryInfo()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aRepository = MakeRecordingRepositoryDtoForAvailableRepository();

            var repositoryAdditionAction = async () => await repositoryService.AddRepositoryAsync(aRepository);

            var repositoryAdditionResult = await repositoryAdditionAction.Should().NotThrowAsync();

            repositoryAdditionResult.Subject.Should().BeEquivalentTo(GetTheRepository());
        }

        [Fact]
        public async Task RemoveRepository_ARecordedRepository_TheRepositoryMustBeRemovedSuccessfully()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aRepository = MakeRemovalRepositoryDtoForAvailableRepository();

            var repositoryRemovalAction = async () => await repositoryService.RemoveRepositoryAsync(aRepository);

            var repositoryRemovalResult = await repositoryRemovalAction.Should().NotThrowAsync();

            repositoryRemovalResult.Subject.Should().BeEquivalentTo(GetTheRepository());
        }

        [Fact]
        public async Task RemoveRepository_AnUnrecordedRepository_AnRelatedExceptionMustBeThrow()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aRepository = MakeRemovalRepositoryDtoForUnavailableRepository();

            var repositoryRemovalAction = async () => await repositoryService.RemoveRepositoryAsync(aRepository);

            var repositoryRemovalResult = await repositoryRemovalAction.Should().ThrowAsync<RepositoryNotFoundException>();
        }


        private static ContainerBuilder RegisterRepositoryServiceAndItsDependencies(ContainerBuilder builder)
        {
            return builder;
        }


        private static StockingBookDto MakeStockingDtoForUnavailableBook()
        {
            return new StockingBookDto
            {

            };
        }

        private static StockingBookDto MakeStockingDtoForAvailableBook()
        {
            return new StockingBookDto
            {

            };
        }


        private static BookReductionDto MakeReductionDtoForAvailableBooks()
        {
            return new BookReductionDto
            {

            };
        }

        private static BookReductionDto MakeReductionDtoForAvailableBooksAndAnUnavailableBook()
        {
            return new BookReductionDto
            {

            };
        }


        private static RecordingRepositoryDto MakeRecordingRepositoryDtoForAvailableRepository()
        {
            return new RecordingRepositoryDto
            {

            };
        }

        private static RecordingRepositoryDto MakeRecordingRepositoryDtoForUnavailableRepository()
        {
            return new RecordingRepositoryDto
            {

            };
        }


        private static RemovalRespositoryDto MakeRemovalRepositoryDtoForAvailableRepository()
        {
            return new RemovalRespositoryDto
            {

            };
        }

        private static RemovalRespositoryDto MakeRemovalRepositoryDtoForUnavailableRepository()
        {
            return new RemovalRespositoryDto
            {

            };
        }


        private static GettingStockBookFilter MakeFilterDtoForStockedBookBaseOnBookId()
        {
            return new GettingStockBookFilter
            {

            };
        }

        private static GettingStockBookFilter MakeFilterDtoForStockedBookBaseOnStockBookId()
        {
            return new GettingStockBookFilter
            {

            };
        }

        private static GettingRepositoriesFilter MakeFilterDtoForGettingRepositoriesBaseOnRepositoryId()
        {
            return new GettingRepositoriesFilter
            {

            };
        }


        private static GettingStockBookFilter MakeFilterDtoForStockedBookBaseOnUnavailableBookId()
        {
            return new GettingStockBookFilter
            {

            };
        }

        private static GettingStockBookFilter MakeFilterDtoForStockedBookBaseOnUnavailableStockBookId()
        {
            return new GettingStockBookFilter
            {

            };
        }

        private static GettingRepositoriesFilter MakeFilterDtoForGettingRepositoriesBaseOnUnavailableRepositoryId()
        {
            return new GettingRepositoriesFilter
            {

            };
        }


        private static IEnumerable<object> GetAllMockStockBook()
        {
            return new object[]
            {

            };
        }

        private static IEnumerable<object> GetAllStockBooksOfSpecificBook()
        {
            return new object[]
            {

            };
        }

        private static IEnumerable<object> GetTheStockBooks()
        {
            return new object[]
            {

            };
        }

        private static IEnumerable<object> GetTheRepositories()
        {
            return new object[]
            {

            };
        }


        private static object GetTheRepository()
        {
            return new
            {
                Id = 1,
            };
        }

        private static long GetStockIdOfAvailableBook()
        {
            return default;
        }

    }
}