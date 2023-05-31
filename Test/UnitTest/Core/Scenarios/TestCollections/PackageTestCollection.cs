using BookShop.Test.UnitTest.Core.Scenarios.CollectionAndTestCaseOrders;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Core.Scenarios.TestCollections
{
    [Order((int)CollectionTestOrder.Package)]
    [CollectionDefinition(nameof(CollectionTestOrder.Package))]
    public class PackageTestCollection
    {
    }
}
