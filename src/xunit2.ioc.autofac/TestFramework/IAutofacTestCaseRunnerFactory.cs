using System.Threading;
using Autofac;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public interface IAutofacTestCaseRunnerFactory
    {
        AutofacTestCaseRunner Create(AutofacTestCase testCase, IContainer container, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, string displayName);
    }

    public class AutofacTestCaseRunnerFactory : IAutofacTestCaseRunnerFactory
    {
        public AutofacTestCaseRunner Create(AutofacTestCase testCase, IContainer container, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, string displayName)
        {
            return new AutofacTestCaseRunner(testCase, container, messageBus, aggregator, cancellationTokenSource, displayName);
        }
    }
}
