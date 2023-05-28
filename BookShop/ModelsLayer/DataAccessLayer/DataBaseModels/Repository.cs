using BookShop.ModelsLayer.DataBaseLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataAccessLayer.DataBaseModels
{
    public class Repository
    {
        public int Id { get; set; }

        public bool IsEnable { get; set; }

        public string Name { get; set; }

        public int? AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}
