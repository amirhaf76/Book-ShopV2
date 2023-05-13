using BookShop.Models.GetwayLayer.RequestResponseModels;

namespace BookShop.Models.Dtos.UserAccountDtos
{
    public class UserCreationResponseDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public DateTime RegeisteredDate { get; set; }
    }
}