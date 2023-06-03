namespace BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.StockVMs
{
    public class StockCreationResultVM
    {
        public long StockId { get; set; }

        public int BookId { get; set; }

        public int RepositoryId { get; set; }

        public int? ReservationId { get; set; }

        public StockStatusVarietyVM Status { get; set; }
    }

    public class StockCreationVM
    {
        public int BookId { get; set; }

        public int RepositoryId { get; set; }

        public int? ReservationId { get; set; }
    }

    

    public enum StockStatusVarietyVM
    {
        New = 1,
        Sold = 2,
        ReTurned = 3,
        Dropped = 4,
    }


}
