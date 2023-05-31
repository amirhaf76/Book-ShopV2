using Autofac;
using Autofac.Extras.Moq;
using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos;
using BookShop.ModelsLayer.Exceptions;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
using BookShop.Test.UnitTest.Core.Scenarios.CollectionAndTestCaseOrders;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios.ServicesScenarios
{
    [Order((int)ServiceTestCollectionScenarioOrder.ReservationServiceTestCases)]
    [Collection(nameof(CollectionTestOrder.Service))]
    public class ReservationServiceTestCases : BaseTestCaseScenario
    {
        private readonly ILogger<ReservationServiceTestCases> _logger;


        public ReservationServiceTestCases(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
            _logger = ResolveService<ILogger<ReservationServiceTestCases>>();
        }


        [Fact]
        public async Task ReserveBook_SettingAnAvailableBookAsReserved_TheBookMustBeReservedAndAccountingShouldBeValid()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var aBook = MakeANewBookReservationDto();

            var reservationAction = async () => await reservationService.ReserveBookAsync(aBook);

            var reservationResult = await reservationAction.Should().NotThrowAsync();

            reservationResult.Subject.Should().NotBeNull();
            reservationResult.Subject.ReservationId.Should().NotBe(default);
        }

        [Fact]
        public async Task ReserveBook_SettingAnReservedBook_JustReturningReservationInfo()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var aBook = MakeBookReservationFromReservedBook();

            var reservationAction = async () => await reservationService.ReserveBookAsync(aBook);

            var reservationResult = await reservationAction.Should().NotThrowAsync();

            reservationResult.Subject.Should().NotBeNull();
            reservationResult.Subject.ReservationId.Should().NotBe(default);
        }

        [Fact]
        public async Task CancelBookReservation_RemovingAReservedBook_TheBookMustBeRemovedAndAccountingShouldBeValid()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var aBook = MakeReservedBookCancellationFromAReservedBook();

            var cancellationAction = async () => await reservationService.CancelBookReservationAsync(aBook);

            var cancellationResult = await cancellationAction.Should().NotThrowAsync();

            cancellationResult.Subject.Should().NotBeNull();
            cancellationResult.Subject.ReservationId.Should().NotBe(default);
        }

        [Fact]
        public async Task CancelBookReservation_RemovingAUnReservedBook_AnRelatedExceptionMustBeThrow()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var aBook = MakeReservedBookCancellationFromAnUnreservedBook();

            var cancellationAction = async () => await reservationService.CancelBookReservationAsync(aBook);

            var cancellationResult = await cancellationAction.Should().ThrowAsync<ReservedBookNotFoundException>();
        }

        [Fact]
        public async Task GetReservedBook_WithoutFilter_AllReservedBookMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var gettingReservationAction = async () => await reservationService.GetReservedBookAsync();

            var gettingReservationResult = await gettingReservationAction.Should().NotThrowAsync();

            gettingReservationResult.Subject.Should().NotBeEmpty().And.BeEquivalentTo(GetAllMockReservedBook());
        }

        [Fact]
        public async Task GetReservedBook_ByAnReservedBookId_TheReservationMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var gettingReservationAction = async () => await reservationService.GetReservedBookAsync(MakeFilterForGettingReservedBook());

            var gettingReservationResult = await gettingReservationAction.Should().NotThrowAsync();

            gettingReservationResult.Subject.Should().NotBeEmpty().And.ContainEquivalentOf(GetObjectFromReservedBook());
        }

        [Fact]
        public async Task GetReservedBook_ByAnBookId_AllReservationOfTheBookMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var gettingReservationAction = async () => await reservationService.GetReservedBookAsync(MakeFilterForGettingSpecificBook());

            var gettingReservationResult = await gettingReservationAction.Should().NotThrowAsync();

            gettingReservationResult.Subject.Should().NotBeEmpty().And.ContainEquivalentOf(GetObjectFromBook());
        }

        [Fact]
        public async Task GetReservedBook_ByUnReservedBookId_AnEmptyCollectionMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var gettingReservationAction = async () => await reservationService.GetReservedBookAsync(MakeFilterForGettingUnreservedBook());

            var gettingReservationResult = await gettingReservationAction.Should().NotThrowAsync();

            gettingReservationResult.Subject.Should().BeEmpty();
        }

        [Fact]
        public async Task GetReservedBook_ByUnExistedBookId_AnEmptyCollectionMustBeReceived()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var gettingReservationAction = async () => await reservationService.GetReservedBookAsync(MakeFilterForGettingUnexistedBook());

            var gettingReservationResult = await gettingReservationAction.Should().NotThrowAsync();

            gettingReservationResult.Subject.Should().BeEmpty();
        }


        private static ContainerBuilder RegisterReservationServiceAndItsDependencies(ContainerBuilder builder)
        {
            return builder;
        }

        private static BookReservationDto MakeANewBookReservationDto()
        {
            return new BookReservationDto
            {

            };
        }

        private static BookReservationDto MakeBookReservationFromReservedBook()
        {
            return new BookReservationDto
            {

            };
        }

        private static ReservedBookCancellationDto MakeReservedBookCancellationFromAReservedBook()
        {
            return new ReservedBookCancellationDto
            {

            };
        }

        private static ReservedBookCancellationDto MakeReservedBookCancellationFromAnUnreservedBook()
        {
            return new ReservedBookCancellationDto
            {

            };
        }


        private static IEnumerable<object> GetAllMockReservedBook()
        {
            throw new NotImplementedException();
        }

        private static object MakeFilterForGettingReservedBook()
        {
            throw new NotImplementedException();
        }

        private static object MakeFilterForGettingSpecificBook()
        {
            throw new NotImplementedException();
        }

        private static object MakeFilterForGettingUnreservedBook()
        {
            throw new NotImplementedException();
        }

        private static object MakeFilterForGettingUnexistedBook()
        {
            throw new NotImplementedException();
        }

        private static object GetObjectFromReservedBook()
        {
            throw new NotImplementedException();
        }

        private static object GetObjectFromBook()
        {
            throw new NotImplementedException();
        }
    }
}