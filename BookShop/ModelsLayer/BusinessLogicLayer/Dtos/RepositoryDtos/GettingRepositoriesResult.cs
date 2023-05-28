namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class GettingRepositoriesResult
    {
        public GettingRepositoriesResult()
        {
            Repositories = new List<RepositoryResult>();
        }

        public IEnumerable<RepositoryResult> Repositories { get; set; }
    }
}
