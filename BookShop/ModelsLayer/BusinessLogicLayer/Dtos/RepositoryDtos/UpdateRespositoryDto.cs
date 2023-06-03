namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class UpdateRespositoryDto
    {
        public int Id { get; set; }

        public bool IsEnable { get; set; }

        public string Name { get; set; }

        public int? AddressId { get; set; }
    }
}
