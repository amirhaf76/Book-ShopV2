using Infrastructure.UsefulDtos;

namespace BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.RequestResponseModels
{
    public class GettingRepositoriesFilterVM
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public int? Id { get; set; }

        public bool? IsEnable { get; set; }

        public string Name { get; set; }

        public int? AddressId { get; set; }
    }
}
