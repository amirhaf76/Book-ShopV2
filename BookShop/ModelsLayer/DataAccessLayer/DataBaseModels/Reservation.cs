namespace BookShop.ModelsLayer.DataAccessLayer.DataBaseModels
{
    public class Reservation
    {
        public Reservation()
        {
            Stocks = new List<Stock>();
        }

        public int Id { get; set; }

        public DateTime LastChange{ get; set; }

        public DateTime? ComfirmationTime { get; set; }

        public ReservationStatus Status { get; set; }

        public int UserAccountId { get; set; }

        public UserAccount UserAccount { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}
