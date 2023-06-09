﻿namespace BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.StockVMs
{
    public class StockCreationResultVM
    {
        public long StockId { get; set; }

        public int BookId { get; set; }

        public int RepositoryId { get; set; }

        public int? ReservationId { get; set; }

        public StockStatusVarietyVM Status { get; set; }
    }


}
