using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac
{
    public class AutofacTestCollectionRunner : XunitTestCollectionRunner
    {
        public AutofacTestCollectionRunner(IContainer container, ITestCollection testCollection, IEnumerable<IXunitTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus,
            ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(testCollection, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
        {
            _container = container;
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases)
        {
            return
                new AutofacTestClassRunner(_container, testClass, @class, testCases, _diagnosticMessageSink, MessageBus, TestCaseOrderer, new ExceptionAggregator(Aggregator), CancellationTokenSource,
                        CollectionFixtureMappings)
                    .RunAsync();
        }

        private readonly IContainer _container;
        private readonly IMessageSink _diagnosticMessageSink;
    }
}