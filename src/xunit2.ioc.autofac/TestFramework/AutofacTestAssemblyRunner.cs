using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestAssemblyRunner : XunitTestAssemblyRunner
    {
        private readonly IContainer _container;

        public AutofacTestAssemblyRunner(IContainer container,
                                         ITestAssembly testAssembly,
                                         IEnumerable<IXunitTestCase> testCases,
                                         IMessageSink diagnosticMessageSink,
                                         IMessageSink executionMessageSink,
                                         ITestFrameworkExecutionOptions executionOptions)
            : base(testAssembly, testCases, diagnosticMessageSink, executionMessageSink, executionOptions)
        {
            _container = container;
        }

        protected override string GetTestFrameworkDisplayName() => "Autofac Test Framework";

        protected override async Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus,
                                                                         ITestCollection testCollection,
                                                                         IEnumerable<IXunitTestCase> testCases,
                                                                         CancellationTokenSource cancellationTokenSource)
        {
            using (var testCollectionLifetimeScope =
                _container.BeginLifetimeScope(AutofacTestScopes.TestCollection, builder => builder.RegisterCollectionFixturesAndModules(testCollection)))
            {
                return await new AutofacTestCollectionRunner(testCollectionLifetimeScope, testCollection, testCases, DiagnosticMessageSink, messageBus,
                                                             TestCaseOrderer, new ExceptionAggregator(Aggregator), cancellationTokenSource).RunAsync();
            }
        }
    }
}
