using Infrastructure.AutoFac.FlagInterface;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models.DataBaseLayer.DataBaseModels
{
    public class UserAccount
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RegeisteredDate { get; set; }
    }
}
