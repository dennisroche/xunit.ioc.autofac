using Xunit.Abstractions;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTest : LongLivedMarshalByRefObject, ITest
    {
        public AutofacTest(AutofacTestCase testCase, string displayName)
        {
            TestCase = testCase;
            DisplayName = displayName;
        }

        public string DisplayName { get; }
        public AutofacTestCase TestCase { get; }

        ITestCase ITest.TestCase => TestCase;
    }
}