using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestAssemblyRunner : TestAssemblyRunner<AutofacTestCase>
    {
        public AutofacTestAssemblyRunner(IContainer container, ITestAssembly testAssembly, IEnumerable<AutofacTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageSink executionMessageSink,
            ITestFrameworkExecutionOptions executionOptions)
            : base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
        {
            _container = container;
        }

        protected override string GetTestFrameworkDisplayName()
        {
            return "Autofac Test Framework";
        }

        protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus, ITestCollection testCollection, IEnumerable<AutofacTestCase> testCases, CancellationTokenSource cancellationTokenSource)
        {
            return new AutofacTestCollectionRunner(_container, testCollection, testCases, DiagnosticMessageSink, messageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource)
                .RunAsync();
        }

        private readonly IContainer _container;
    }
}