using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestMethodRunner : XunitTestMethodRunner
    {
        private readonly IMessageSink _diagnosticMessageSink;

        private readonly ILifetimeScope _testClassLifetimeScope;

        public AutofacTestMethodRunner(ILifetimeScope testClassLifetimeScope,
                                       IMessageSink diagnosticMessageSink,
                                       ITestMethod testMethod,
                                       IReflectionTypeInfo @class,
                                       IReflectionMethodInfo method,
                                       IEnumerable<IXunitTestCase> testCases,
                                       IMessageBus messageBus,
                                       ExceptionAggregator aggregator,
                                       CancellationTokenSource cancellationTokenSource,
                                       object[] constructorArguments)
            : base(testMethod, @class, method, testCases, diagnosticMessageSink, messageBus, aggregator, cancellationTokenSource, constructorArguments)
        {
            _testClassLifetimeScope = testClassLifetimeScope;
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        protected override async Task<RunSummary> RunTestCaseAsync(IXunitTestCase testCase)
        {
            if (testCase is AutofacTestCase autofacTestCase) autofacTestCase.TestClassLifetimeScope = _testClassLifetimeScope;

            return await testCase.RunAsync(_diagnosticMessageSink, MessageBus, new object[] { }, Aggregator, CancellationTokenSource);
        }
    }
}
