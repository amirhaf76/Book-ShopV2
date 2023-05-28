namespace BookShop.Test.UnitTest.TestingMoqPackageModels
{
    public class TestController
    {
        private readonly ISomeService _service;

        public TestController(ISomeService service)
        {
            _service = service;
        }

        public string Index()
        {
            var something = _service.SomeMethod("something");
            var somethingElse = _service.SomeOtherMethod();
            return $"{something}-{somethingElse}";
        }
    }


}
