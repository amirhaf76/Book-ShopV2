namespace BookShop.ModelsLayer.Exceptions
{
    public class RepositoryNotFoundException : Exception
    {
        const string ERROR_MESSAGE = "The repository is not found!";
        public RepositoryNotFoundException() : base(ERROR_MESSAGE)
        {

        }

        public RepositoryNotFoundException(int id) : base($"{ERROR_MESSAGE} Id: {id}")
        {

        }
    }
}
