using System.Runtime.Serialization;

namespace BookShop.ModelsLayer.DataAccessLayer.ExceptionModels
{
    public class UnsuccessfulCancellingReservationException : Exception
    {
        public UnsuccessfulCancellingReservationException()
        {
        }

        public UnsuccessfulCancellingReservationException(string message) : base(message)
        {
        }

        public UnsuccessfulCancellingReservationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsuccessfulCancellingReservationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
