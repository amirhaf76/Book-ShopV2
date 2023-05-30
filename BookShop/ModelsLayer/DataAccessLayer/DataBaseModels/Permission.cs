namespace BookShop.ModelsLayer.DataAccessLayer.DataBaseModels
{
    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserAccount> UserAccounts { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
