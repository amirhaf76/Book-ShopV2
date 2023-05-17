namespace BookShop.ModelsLayer.Dtos.UserAccountDtos
{
    public class UserCreationResponseDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public DateTime RegeisteredDate { get; set; }
    }
}