﻿namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.RepositoryDtos
{
    public class RepositoryResult
    {
        public int Id { get; set; }

        public bool IsEnable { get; set; }

        public string Name { get; set; }

        public int? AddressId { get; set; }
    }
}
