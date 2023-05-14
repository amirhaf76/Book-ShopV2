namespace BookShop.ModelsLayer.DataBaseLayer.DataBaseModels
{
    public class Province
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;


        public ICollection<City> Cities { get; set; }


        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
