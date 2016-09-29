using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestCollectionRunner : TestCollectionRunner<AutofacTestCase>
    {
        public AutofacTestCollectionRunner(IContainer container, ITestCollection testCollection, IEnumerable<AutofacTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus,
            ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
            : base(testCollection, testCases, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
        {
            _container = container;
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo reflectionTypeInfo, IEnumerable<AutofacTestCase> testCases)
        {
            var exceptionAggregator = new ExceptionAggregator(Aggregator);

            var autofacTestClassRunner = new AutofacTestClassRunner(_container, testClass, reflectionTypeInfo, testCases, 
                _diagnosticMessageSink, MessageBus, TestCaseOrderer, exceptionAggregator, 
                CancellationTokenSource);

            return autofacTestClassRunner.RunAsync();
        }

        private readonly IContainer _container;
        private readonly IMessageSink _diagnosticMessageSink;
    }
}