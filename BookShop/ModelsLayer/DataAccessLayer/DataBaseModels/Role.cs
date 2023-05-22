namespace BookShop.ModelsLayer.DataAccessLayer.DataBaseModels
{
    public class Role
    {
        public Role()
        {
            Permissions = new List<Permission>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Permission> Permissions { get; set; }

    }
}
