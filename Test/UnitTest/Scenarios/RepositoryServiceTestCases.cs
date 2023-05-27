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

            var aBook = MakeReductionDtoForAvailableBooksAndAnUnavailableBook();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync(MakeFilterDtoForStockedBook());

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEquivalentTo(GetAllMockStockBook());
        }

        [Fact]
        public async Task GetBooks_ByAnAvailableBookId_TheBookWithAllItsInstanceMustBeReceivedSuccessfully()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aBook = MakeReductionDtoForAvailableBooksAndAnUnavailableBook();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync(MakeFilterDtoForStockedBookBaseOnBookId());

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEquivalentTo(GetAllStockBooksOfSpecificBook());
        }

        [Fact]
        public async Task GetBooks_ByAnUnavailableBookId_AnEmptyCollectionMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aBook = MakeReductionDtoForAvailableBooksAndAnUnavailableBook();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync(MakeFilterDtoForStockedBookBaseOnBookId());

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBooks_ByAnAvailableBookInstanceId_TheBookInstanceMustBeReceivedSuccessfull()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aBook = MakeReductionDtoForAvailableBooksAndAnUnavailableBook();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync(MakeFilterDtoForStockedBookBaseOnStockBookId());

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEquivalentTo(GetTheStockBook());
        }

        [Fact]
        public async Task GetBooks_ByAnUnavailableBookInstanceId_AnEmptyCollectionMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterRepositoryServiceAndItsDependencies(builder));

            var repositoryService = mock.Create<IRepositoryService>();

            var aBook = MakeReductionDtoForAvailableBooksAndAnUnavailableBook();

            var gettingStockBooksAction = async () => await repositoryService.GetStockBookAsync(MakeFilterDtoForStockedBookBaseOnStockBookId());

            var gettingStockBooksResult = await gettingStockBooksAction.Should().NotThrowAsync();

            gettingStockBooksResult.Subject.StockBooks.Should().BeEmpty();
        }

        [Fact]
        public async Task GetRepositories_WithoutFilter_AllRepositoriesMustBeReceived()
        {
        }

        [Fact]
        public async Task GetRepositories_ByAnAvailableRepositoryId_TheRepositoryMustBeReceivedSuccessfully()
        {
        }

        [Fact]
        public async Task GetRepositories_ByAnUnavailableRepositoryId_AnEmptyCollectionMustBeReceived()
        {
        }

        [Fact]
        public async Task AddRepository_AnUnRecordedRepository_TheRepositoryMustBeAddedSuccessfully()
        {
        }

        [Fact]
        public async Task AddRepository_AnRecordedRepository_JustReturnRecordedRepositoryInfo()
        {
        }

        [Fact]
        public async Task RemoveRepository_AnRecordedRepository_TheRepositoryMustBeRemovedSuccessfully()
        {
        }

        [Fact]
        public async Task RemoveRepository_AnUnRecordedInstanceRepository_AnRelatedExceptionMustBeThrow()
        {
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

        private static GettingStockBookFilter MakeFilterDtoForStockedBook()
        {
            return new GettingStockBookFilter
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

        private static IEnumerable<object> GetTheStockBook()
        {
            return new object[]
            {

            };
        }

        private static long GetStockIdOfAvailableBook()
        {
            return default;
        }

    }
}