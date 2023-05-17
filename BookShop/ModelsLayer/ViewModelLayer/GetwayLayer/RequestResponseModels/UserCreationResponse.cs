namespace BookShop.ModelsLayer.GetwayLayer.RequestResponseModels
{
    public class UserCreationResponse
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public DateTime RegeisteredDate { get; set; }
    }
}