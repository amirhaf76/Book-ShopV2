using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.ModelsLayer.DataBaseLayer.DataBaseModels
{
    public class UserAccount
    {
        public UserAccount()
        {
            Permissions = new List<Permission>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime RegeisteredDate { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
