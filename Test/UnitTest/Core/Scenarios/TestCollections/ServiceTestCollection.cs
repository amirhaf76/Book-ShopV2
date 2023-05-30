using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Core.Scenarios
{
    [Order((int)CollectionTestOrder.Service)]
    [CollectionDefinition(nameof(CollectionTestOrder.Service))]
    public class ServiceTestCollection
    {
    }
}
