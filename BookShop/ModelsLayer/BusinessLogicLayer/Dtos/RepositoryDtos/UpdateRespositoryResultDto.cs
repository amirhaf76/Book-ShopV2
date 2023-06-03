namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class UpdateRespositoryResultDto
    {
        public int Id { get; set; }

        public bool IsEnable { get; set; }

        public string Name { get; set; }

        public int? AddressId { get; set; }
    }
}
