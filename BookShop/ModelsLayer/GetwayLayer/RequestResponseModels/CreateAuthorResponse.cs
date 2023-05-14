namespace BookShop.ModelsLayer.GetwayLayer.RequestResponseModels
{
    public class CreateAuthorResponse
    {
        public int? Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
