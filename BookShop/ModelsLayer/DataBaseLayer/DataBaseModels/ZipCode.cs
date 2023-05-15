using System.ComponentModel.DataAnnotations;

namespace BookShop.ModelsLayer.DataBaseLayer.DataBaseModels
{
    public class ZipCode
    {
        public int Id { get; set; }

        public long Code { get; set; }


        public int? CityId { get; set; }

        public City City { get; set; }


        public int? ProvinceId { get; set; }

        public Province Province { get; set; }


        public int? CountryId { get; set; }

        public Country Country { get; set; }
    }

}
