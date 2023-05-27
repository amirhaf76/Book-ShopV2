using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
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

        public async Task AddBook_AnUnRecordedInstanceBook_TheBookMustBeAddedSuccessfully()
        {
        }

        public async Task AddBook_AnRecordedInstanceBook_JustReturnRecordedBookInfo()
        {
        }

        public async Task RemoveBook_AnRecordedInstanceBook_TheBookMustBeRemovedSuccessfully()
        {
        }

        public async Task RemoveBook_AnUnRecordedInstanceBook_AnRelatedExceptionMustBeThrow()
        {
        }

        public async Task GetBooks_WithoutFilter_AllBooksMustBeReceived()
        {
        }

        public async Task GetBooks_ByAnAvailableBookId_TheBookWithAllItsInstanceMustBeReceivedSuccessfully()
        {
        }

        public async Task GetBooks_ByAnUnavailableBookId_AnEmptyCollectionMustBeReceived()
        {
        }

        public async Task GetBooks_ByAnAvailableBookInstanceId_TheBookInstanceMustBeReceivedSuccessfull()
        {
        }

        public async Task GetBooks_ByAnUnavailableBookInstanceId_AnEmptyCollectionMustBeReceived()
        {
        }

        public async Task GetRepositories_WithoutFilter_AllRepositoriesMustBeReceived()
        {
        }

        public async Task GetRepositories_ByAnAvailableRepositoryId_TheRepositoryMustBeReceivedSuccessfully()
        {
        }

        public async Task GetRepositories_ByAnUnavailableRepositoryId_AnEmptyCollectionMustBeReceived()
        {
        }

        public async Task AddRepository_AnUnRecordedRepository_TheRepositoryMustBeAddedSuccessfully()
        {
        }

        public async Task AddRepository_AnRecordedRepository_JustReturnRecordedRepositoryInfo()
        {
        }

        public async Task RemoveRepository_AnRecordedRepository_TheRepositoryMustBeRemovedSuccessfully()
        {
        }

        public async Task RemoveRepository_AnUnRecordedInstanceRepository_AnRelatedExceptionMustBeThrow()
        {
        }
    }
}