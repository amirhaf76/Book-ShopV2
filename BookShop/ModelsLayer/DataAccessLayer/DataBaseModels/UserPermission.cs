using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataAccessLayer.DataBaseModels
{
    public class UserPermission
    {
        public int UserId { get; set; }

        public UserAccount UserAccount { get; set; }


        public int PermissionId { get; set; }

        public Permission Permission { get; set; }


        public int? RoleId { get; set; }

        public Role Role { get; set; }
    }
}
