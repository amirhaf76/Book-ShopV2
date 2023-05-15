using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.ModelsLayer.DataBaseLayer.DataBaseModels
{
    public class UserAccount
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime RegeisteredDate { get; set; }
    }
}
