using Autofac;
using Autofac.Extras.Moq;
using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServices;
using BookShop.ModelsLayer.BusinessLogicLayer.BusinessServicesAbstraction;
using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.AuthorDtos;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.MockRepositoryPattern;
using BookShop.Test.UnitTest.Core.Scenarios;
using BookShop.Test.UnitTest.Core.Scenarios.CollectionAndTestCaseOrders;
using BookShop.Test.UnitTest.SettingsModels;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios.ServicesScenarios
{
    [Order((int)ServiceTestCollectionScenarioOrder.AuthorServiceTestCases)]
    [Collection(nameof(CollectionTestOrder.Service))]
    public class AuthorServiceTestCases : BaseTestCaseScenario
    {
        private readonly DelaySettings _delaySettings;
        private readonly ILogger<AuthorServiceTestCases> _logger;
        private int _primaryKey = 0;

        public AuthorServiceTestCases(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
            _logger = ResolveService<ILogger<AuthorServiceTestCases>>();
            _delaySettings = ResolveService<DelaySettings>();
        }


        [Fact]
        public async Task AddAuthorIfItDoesntExistAsync_AddAuthor_TheAuthorMustBeAddedSuccessfuly()
        {
            var authors = new Dictionary<int, Author>()
            {
                { 2, new Author {Id = 2,FirstName = "TestUser",LastName = "TestUserSecond" } },
            };

            using var mock = AutoMock.GetStrict(builder => RegisterAuthorService(builder, authors));

            var baseRepository = Mock.Get(mock.Create<IAuthorRepository>()).As<IBaseRepository<Author>>();

            var authorService = mock.Create<IAuthorService>();

            var newAuthor = new AuthorDto
            {
                Id = 4,
                FirstName = "TestUser",
                LastName = "TestUserSecond",
            };

            var authorCountBeforeAddition = authors.Count;

            var addedAuthor = await authorService.AddAuthorIfItDoesntExistAsync(newAuthor);

            baseRepository.Verify(repo => repo.AddAsync(It.Is<Author>(e => e.Id == newAuthor.Id)), Times.Once);

            authors.Should().HaveCount(authorCountBeforeAddition + 1);
            authors.Should().ContainKey(newAuthor.Id);
            authors[newAuthor.Id].FirstName.Should().Be(newAuthor.FirstName);
            authors[newAuthor.Id].LastName.Should().Be(newAuthor.LastName);
            authors[newAuthor.Id].Id.Should().Be(newAuthor.Id);

        }

        [Fact]
        public async Task AddAuthorIfItDoesntExistAsync_AddAuthor_TheAuthorMustBeAddedSuccessfuly2()
        {
            var authors = new Dictionary<int, Author>()
            {
                { 2, new Author {Id = 2,FirstName = "TestUser",LastName = "TestUserSecond" } },
            };

            using var mock = AutoMock.GetStrict(builder => RegisterAuthorService(builder, authors));

            var baseRepository = Mock.Get(mock.Create<IAuthorRepository>()).As<IBaseRepository<Author>>();

            var authorService = mock.Create<IAuthorService>();

            var newAuthor = new AuthorDto
            {
                Id = 2,
                FirstName = "TestUser3",
                LastName = "TestUserSecond3",
            };

            var authorCountBeforeAddition = authors.Count;

            var addedAuthor = await authorService.AddAuthorIfItDoesntExistAsync(newAuthor);

            baseRepository.Verify(repo => repo.AddAsync(It.IsAny<Author>()), Times.Never);

            authors.Should().HaveCount(authorCountBeforeAddition);
            authors.Should().ContainKey(newAuthor.Id);
            authors[newAuthor.Id].FirstName.Should().NotBe(newAuthor.FirstName);
            authors[newAuthor.Id].LastName.Should().NotBe(newAuthor.LastName);
            authors[newAuthor.Id].Id.Should().Be(newAuthor.Id);

        }



        private ContainerBuilder RegisterAuthorService(ContainerBuilder builder, Dictionary<int, Author> authorsMap)
        {
            builder.RegisterMock(CreateMockIAuthorRepository(authorsMap));

            builder.RegisterType<AuthorService>().As<IAuthorService>();

            return builder;
        }

        private Mock<IAuthorRepository> CreateMockIAuthorRepository(Dictionary<int, Author> authors)
        {
            var mockIAuthorRepository = new MockRepositoryBuilder<int, Author>(x => x.Id, (k, r) => r.Id = k, () => _primaryKey++, authors)
                .CreateMockBaseRepository<IAuthorRepository>();

            mockIAuthorRepository
                .Setup(x => x.FindAuthorOrDefaultAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) =>
                {
                    var mockIBaseRepository = mockIAuthorRepository.As<IBaseRepository<Author>>();

                    return mockIBaseRepository.Object.Find(id);
                });
            return mockIAuthorRepository;
        }
    }
}