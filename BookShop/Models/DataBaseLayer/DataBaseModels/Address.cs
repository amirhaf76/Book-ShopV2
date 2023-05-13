using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models.DataBaseLayer.DataBaseModels
{
    public class Address
    {
        public int Id { get; set; }

        [ForeignKey("ZipCodeEdm")]
        public ZipCode ZipCode { get; set; }

        public string AddressLine { get; set; } = string.Empty;
    }
}
