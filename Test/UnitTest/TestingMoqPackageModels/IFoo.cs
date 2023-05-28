namespace BookShop.Test.UnitTest.TestingMoqPackageModels
{
    public interface IFoo
    {
        public Bar Bar { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool DoSomething(string value);
        public bool DoSomething(int number, string value);
        public Task<bool> DoSomethingAsync();
        public string DoSomethingStringy(string value);
        public bool TryParse(string value, out string outputValue);
        public bool Submit(ref Bar bar);
        public int GetCount();
        public bool Add(int value);
    }


}
