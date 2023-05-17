namespace BookShop.ModelsLayer.DtosExtension
{
    public class ConvertingDtoException<TFrom, TTo> : Exception
    {
        private const string ERROR_FROM_TO = "Converting from '{0}' to '{1}' was failed!";
        private const string REASON = " it's because of {2}.";

        public ConvertingDtoException() : base(string.Format(ERROR_FROM_TO, nameof(TFrom), nameof(TTo)))
        {

        }

        public ConvertingDtoException(string reason) : base(string.Format(ERROR_FROM_TO + REASON, nameof(TFrom), nameof(TTo), reason))
        {

        }
    }
}
