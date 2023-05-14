using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.ModelsLayer.DataBaseLayer.DataBaseModels
{
    public class Address
    {
        public int Id { get; set; }

        [ForeignKey("ZipCodeEdm")]
        public ZipCode ZipCode { get; set; }

        public string AddressLine { get; set; } = string.Empty;
    }
}
