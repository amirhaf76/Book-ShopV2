using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Core.Scenarios
{
    [Order((int)CollectionTestOrder.Repository)]
    [CollectionDefinition(nameof(CollectionTestOrder.Repository))]
    public class RepositoryTestCollection
    {
    }
}
