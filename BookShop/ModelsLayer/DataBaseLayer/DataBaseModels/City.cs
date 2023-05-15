namespace BookShop.ModelsLayer.DataBaseLayer.DataBaseModels
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumberCode { get; set; }

        public int ProvinceId { get; set; }

        public Province Province { get; set; }
    }
}
