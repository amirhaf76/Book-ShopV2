namespace BookShop.ModelsLayer.BusinessLogicLayer.SemaphorModels
{

    public class BinarySemaphore
    {
        /// <summary>
        /// Default start as One : BinarySemaphore(startAsZero: false).
        /// </summary>
        public BinarySemaphore() : this(false)
        {
        }

        public BinarySemaphore(bool startAsZero)
        {
            Semaphore = new Semaphore(startAsZero ? 0 : 1, 1);
        }
        public Semaphore Semaphore { get; private set; }
    }
}
