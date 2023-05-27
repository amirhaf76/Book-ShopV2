using Autofac;
using Autofac.Extras.Moq;
using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.BookDtos;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios
{
    [Order((int)ScenariosOrder.ReservationServiceTestCases)]
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

            var aBook = MakeBookReservationForReservedBook();

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

            var aBook = MakeReservedBookCancellationDto();

            var reservationAction = async () => await reservationService.CancelBookReservationAsync(aBook);

            var reservationResult = await reservationAction.Should().NotThrowAsync();

            reservationResult.Subject.Should().NotBeNull();
            reservationResult.Subject.ReservationId.Should().NotBe(default);
        }

        [Fact]
        public async Task CancelBookReservation_RemovingAUnReservedBook_AnRelatedExceptionMustBeThrow()
        {
            using var mock = AutoMock.GetStrict(builder => RegisterReservationServiceAndItsDependencies(builder));

            var reservationService = mock.Create<IReservationService>();

            var aBook = MakeReservedBookCancellationDto();

            var reservationAction = async () => await reservationService.CancelBookReservationAsync(aBook);

            var reservationResult = await reservationAction.Should().ThrowAsync<ReservedBookNotFoundException>();
        }

        [Fact]
        public async Task GetReservedBook_WithoutFilter_AllReservedBookMustBeReceived()
        {
        }

        [Fact]
        public async Task GetReservedBook_ByAnReservedBookId_TheReservationMustBeReceived()
        {
        }

        [Fact]
        public async Task GetReservedBook_ByAnBookId_AllReservationOfTheBookMustBeReceived()
        {
        }

        [Fact]
        public async Task GetReservedBook_ByUnReservedBookId_AnEmptyCollectionMustBeReceived()
        {
        }

        [Fact]
        public async Task GetReservedBook_ByUnBookId_AnEmptyCollectionMustBeReceived()
        {
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

        private static ReservedBookCancellationDto MakeReservedBookCancellationDto()
        {
            return new ReservedBookCancellationDto
            {

            };
        }

        private static BookReservationDto MakeBookReservationForReservedBook()
        {
            return new BookReservationDto
            {

            };
        }
    }
}