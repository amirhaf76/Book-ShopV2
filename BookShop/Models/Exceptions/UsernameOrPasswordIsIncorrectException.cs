using System.Runtime.Serialization;

namespace BookShop.Models.Exceptions
{
    [Serializable]
    public class UsernameOrPasswordIsIncorrectException : Exception
    {
        public UsernameOrPasswordIsIncorrectException() : base()
        {

        }

        public UsernameOrPasswordIsIncorrectException(string message) : base(message)
        {

        }

        public UsernameOrPasswordIsIncorrectException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public UsernameOrPasswordIsIncorrectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
    

}
