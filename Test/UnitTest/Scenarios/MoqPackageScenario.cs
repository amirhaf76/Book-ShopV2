using BookShop.Test.UnitTest.Core.AppConfigModel;
using BookShop.Test.UnitTest.Core.Scenarios;
using BookShop.Test.UnitTest.TestingMoqPackageModels;
using Moq;
using System.Text.RegularExpressions;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace BookShop.Test.UnitTest.Scenarios
{
    [Order((int)DefaultTestCollectionScenarioOrder.MoqPackageScenario)]
    [Collection(nameof(CollectionTestOrder.Service))]
    public class MoqPackageScenario : BaseTestCaseScenario
    {
        private static Mock<IFoo> mock = new Mock<IFoo>();
        public MoqPackageScenario(AppConfiguration totalConfiguration, ITestOutputHelper testOutputHelper) : base(totalConfiguration, testOutputHelper)
        {
        }

        [Fact]
        public void TestSampleTestCase1()
        {
            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);


            // out arguments
            var outString = "ack";
            // TryParse will return true, and the out argument will return "ack", lazy evaluated
            mock.Setup(foo => foo.TryParse("ping", out outString)).Returns(true);


            // ref arguments
            var instance = new Bar();
            // Only matches if the ref argument to the invocation is the same instance
            mock.Setup(foo => foo.Submit(ref instance)).Returns(true);


            // access invocation arguments when returning a value
            mock.Setup(x => x.DoSomethingStringy(It.IsAny<string>()))
                    .Returns((string s) => s.ToLower());
            // Multiple parameters overloads available


            // throwing when invoked with specific parameters
            mock.Setup(foo => foo.DoSomething("reset")).Throws<InvalidOperationException>();
            mock.Setup(foo => foo.DoSomething("")).Throws(new ArgumentException("command"));


            // lazy evaluating return value
            var count = 1;
            mock.Setup(foo => foo.GetCount()).Returns(() => count);


            // async methods (see below for more about async):
            mock.Setup(foo => foo.DoSomethingAsync()).ReturnsAsync(true);
            // Or
            mock.Setup(foo => foo.DoSomethingAsync()).Returns(async () => await Task.FromResult(true));

            // In Moq 4.16 you can use :
            // !!! mock.Setup(foo => foo.DoSomethingAsync().Result).Returns(true); !!!
        }

        [Fact]
        public void TestSampleTestCase2()
        {
            // any value
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(true);


            // any value passed in a `ref` parameter (requires Moq 4.8 or later):
            // !!! mock.Setup(foo => foo.Submit(ref It.Ref<Bar>.IsAny)).Returns(true); !!!


            // matching Func<int>, lazy evaluated
            mock.Setup(foo => foo.Add(It.Is<int>(i => i % 2 == 0))).Returns(true);


            // matching ranges
            mock.Setup(foo => foo.Add(It.IsInRange<int>(0, 10, Moq.Range.Inclusive))).Returns(true);


            // matching regex
            mock.Setup(x => x.DoSomethingStringy(It.IsRegex("[a-d]+", RegexOptions.IgnoreCase))).Returns("foo");
        }

        [Fact]
        public void TestSampleTestCase3()
        {
            mock.Setup(foo => foo.Name).Returns("bar");


            // auto-mocking hierarchies (a.k.a. recursive mocks)
            mock.Setup(foo => foo.Bar.Baz.Name).Returns("baz");

            // expects an invocation to set the value to "foo"
            mock.SetupSet(foo => foo.Name = "foo");

            // or verify the setter directly
            mock.Object.Name = "foo";
            mock.VerifySet(foo => foo.Name = "foo");
        }

        [Fact]
        public void TestSampleTestCase4()
        {
            // start "tracking" sets/gets to this property
            mock.SetupProperty(f => f.Name);

            // alternatively, provide a default value for the stubbed property
            mock.SetupProperty(f => f.Name, "foo");


            // Now you can do:

            IFoo foo = mock.Object;
            // Initial value was stored
            Assert.Equal("foo", foo.Name);

            // New value set which changes the initial value
            foo.Name = "bar";
            Assert.Equal("bar", foo.Name);
        }

        [Fact]
        public void TestSampleTestCase5()
        {
            mock.SetupAllProperties();
        }
    }


}