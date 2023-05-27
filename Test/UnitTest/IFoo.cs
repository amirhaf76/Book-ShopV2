namespace BookShop.Test.UnitTest
{
    public interface IFoo
    {
        Bar Bar { get; set; }
        string Name { get; set; }
        int Value { get; set; }
        bool DoSomething(string value);
        bool DoSomething(int number, string value);
        Task<bool> DoSomethingAsync();
        string DoSomethingStringy(string value);
        bool TryParse(string value, out string outputValue);
        bool Submit(ref Bar bar);
        int GetCount();
        bool Add(int value);


    }

    public class Bar
    {
        public virtual Baz Baz { get; set; }
        public virtual bool Submit() { return false; }
    }

    public class Baz
    {
        public virtual string Name { get; set; }
    }

    public interface ISomeService
    {
        string SomeMethod(string parameter);
        string SomeOtherMethod();
    }

    public class TestController
    {
        private readonly ISomeService _service;

        public TestController(ISomeService service)
        {
            _service = service;
        }

        public string Index()
        {
            var something = this._service.SomeMethod("something");
            var somethingElse = this._service.SomeOtherMethod();
            return $"{something}-{somethingElse}";
        }
    }

    
}
