namespace BookShop.Models.GetwayLayer.RequestResponseModels
{
    public class CreateAuthorRequest
    {
        public IEnumerable<CreateAuthorSubRequest> AuthorInfos { get; set; } = Array.Empty<CreateAuthorSubRequest>();
    }
}
