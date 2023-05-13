using System.Runtime.Serialization;

namespace BookShop.Models.Exceptions
{
    [Serializable]
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException() : base()
        {

        }

        public AuthorNotFoundException(string message) : base(message)
        {

        }

        public AuthorNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public AuthorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }

}
