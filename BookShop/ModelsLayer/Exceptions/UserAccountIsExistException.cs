using System.Runtime.Serialization;

namespace BookShop.ModelsLayer.Exceptions
{
    [Serializable]
    public class UserAccountIsExistException : Exception
    {
        public UserAccountIsExistException() : base()
        {

        }

        public UserAccountIsExistException(string message) : base(message)
        {

        }

        public UserAccountIsExistException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public UserAccountIsExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }


}
