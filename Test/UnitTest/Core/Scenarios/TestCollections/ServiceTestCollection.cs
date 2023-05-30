using BookShop.Test.UnitTest.Core.Scenarios.CollectionAndTestCaseOrders;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Core.Scenarios.TestCollections
{
    [Order((int)CollectionTestOrder.Service)]
    [CollectionDefinition(nameof(CollectionTestOrder.Service))]
    public class ServiceTestCollection
    {
    }
}
