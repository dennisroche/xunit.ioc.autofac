using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public interface IAutofacTestCaseFactory
    {
        AutofacTestCase Create(TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod);
    }

}
