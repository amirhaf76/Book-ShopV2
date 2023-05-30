namespace BookShop.ModelsLayer.DataAccessLayer.DataBaseModels
{
    public class Stock
    {
        public long StockId { get; set; }


        public int RepositoryId { get; set; }

        public Repository Repository { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int? ReservationId { get; set; }

        public Reservation Reservation { get; set; }


        public StockStatus Status { get; set; }
    }
}
