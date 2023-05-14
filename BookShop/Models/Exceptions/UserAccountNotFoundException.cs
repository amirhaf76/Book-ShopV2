using System.Runtime.Serialization;

namespace BookShop.Models.Exceptions
{
    [Serializable]
    public class UserAccountNotFoundException : Exception
    {
        public UserAccountNotFoundException() : base()
        {

        }

        public UserAccountNotFoundException(string message) : base(message)
        {

        }

        public UserAccountNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public UserAccountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
    

}
