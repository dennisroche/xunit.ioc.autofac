using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestMethodRunner : TestMethodRunner<AutofacTestCase>
    {
        public AutofacTestMethodRunner(IContainer container, IMessageSink diagnosticMessageSink, ITestMethod testMethod, IReflectionTypeInfo @class, IReflectionMethodInfo method, IEnumerable<AutofacTestCase> testCases, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource) 
            : base(testMethod, @class, method, testCases, messageBus, aggregator, cancellationTokenSource)
        {
            _container = container;
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        protected override Task<RunSummary> RunTestCaseAsync(AutofacTestCase testCase)
        {
            return testCase.RunAsync(_container, _diagnosticMessageSink, MessageBus, Aggregator, CancellationTokenSource);
        }

        private readonly IContainer _container;
        private readonly IMessageSink _diagnosticMessageSink;
    }
}