using BookShop.Test.UnitTest.Core.Scenarios.CollectionAndTestCaseOrders;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Core.Scenarios.TestCollections
{
    [Order((int)CollectionTestOrder.Repository)]
    [CollectionDefinition(nameof(CollectionTestOrder.Repository))]
    public class RepositoryTestCollection
    {
    }
}
