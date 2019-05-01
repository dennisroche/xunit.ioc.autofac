namespace Xunit.Ioc.Autofac.TestFramework
{
    public interface IAutofacTestFactory
    {
        AutofacTest Create(AutofacTestCase testCase, string displayName);
    }
}
