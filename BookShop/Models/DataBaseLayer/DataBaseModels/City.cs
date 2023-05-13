namespace BookShop.Models.DataBaseLayer.DataBaseModels
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PhoneNumberCode { get; set; } = string.Empty;

        public int ProvinceId { get; set; }

        public Province Province { get; set; }
    }
}
