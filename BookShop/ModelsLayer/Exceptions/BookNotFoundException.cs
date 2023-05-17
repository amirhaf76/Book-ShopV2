using System.Runtime.Serialization;

namespace BookShop.ModelsLayer.Exceptions
{
    [Serializable]
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException() : base()
        {

        }

        public BookNotFoundException(string message) : base(message)
        {

        }

        public BookNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public BookNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
