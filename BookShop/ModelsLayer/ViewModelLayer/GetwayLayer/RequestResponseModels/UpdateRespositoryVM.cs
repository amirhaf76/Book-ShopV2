namespace BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.RequestResponseModels
{
    public class UpdateRespositoryVM
    {
        public int Id { get; set; }

        public bool IsEnable { get; set; }

        public string Name { get; set; }

        public int? AddressId { get; set; }
    }
}
