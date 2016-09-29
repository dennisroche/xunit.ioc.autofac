using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestCase : TestMethodTestCase
    {
        public AutofacTestCase() { }

        public AutofacTestCase(TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod)
            : base(defaultMethodDisplay, testMethod) { }

        public Task<RunSummary> RunAsync(IContainer container, IMessageSink diagnosticMessageSink, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            return new AutofacTestCaseRunner(this, container, messageBus, aggregator, cancellationTokenSource, DisplayName)
                .RunAsync();
        }
    }
}