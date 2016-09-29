using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestClassRunner : TestClassRunner<AutofacTestCase>
    {
        private readonly IContainer _container;

        public AutofacTestClassRunner(IContainer container, ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<AutofacTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus, ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource) 
            : base(testClass, @class, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
        {
            _container = container;
        }

        protected override Task<RunSummary> RunTestMethodAsync(ITestMethod testMethod, IReflectionMethodInfo method, IEnumerable<AutofacTestCase> testCases, object[] constructorArguments)
        {
            return new AutofacTestMethodRunner(_container, DiagnosticMessageSink, testMethod, Class, method, testCases, MessageBus, new ExceptionAggregator(Aggregator), CancellationTokenSource)
                .RunAsync();
        }
    }
}
