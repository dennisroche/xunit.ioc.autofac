using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestCaseFactory : IAutofacTestCaseFactory
    {
        public AutofacTestCase Create(TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod)
        {
            return new AutofacTestCase(defaultMethodDisplay, testMethod);
        }
    }

}
