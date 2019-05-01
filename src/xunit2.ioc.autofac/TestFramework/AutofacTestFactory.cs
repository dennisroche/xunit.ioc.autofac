namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestFactory : IAutofacTestFactory
    {
        public AutofacTest Create(AutofacTestCase testCase, string displayName)
        {
            return new AutofacTest(testCase, displayName);
        }
    }
}
