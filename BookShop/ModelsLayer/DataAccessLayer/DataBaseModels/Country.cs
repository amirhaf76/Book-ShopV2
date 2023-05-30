namespace BookShop.ModelsLayer.DataAccessLayer.DataBaseModels
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public ICollection<Province> Provinces { get; set; }
    }
}
