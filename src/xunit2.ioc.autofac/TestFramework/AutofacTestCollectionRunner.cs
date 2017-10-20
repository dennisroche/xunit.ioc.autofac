using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestCollectionRunner : XunitTestCollectionRunner
    {
        private readonly IMessageSink _diagnosticMessageSink;
        private readonly ILifetimeScope _testCollectionLifetimeScope;

        public AutofacTestCollectionRunner(ILifetimeScope testCollectionLifetimeScope,
                                           ITestCollection testCollection,
                                           IEnumerable<IXunitTestCase> testCases,
                                           IMessageSink diagnosticMessageSink,
                                           IMessageBus messageBus,
                                           ITestCaseOrderer testCaseOrderer,
                                           ExceptionAggregator aggregator,
                                           CancellationTokenSource cancellationTokenSource)
            : base(testCollection, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
        {
            _testCollectionLifetimeScope = testCollectionLifetimeScope;
            _diagnosticMessageSink = diagnosticMessageSink;
        }

        protected override void CreateCollectionFixture(Type fixtureType)
        {
            Aggregator.Run(() => CollectionFixtureMappings[fixtureType] = _testCollectionLifetimeScope.Resolve(fixtureType));
        }

        protected override async Task<RunSummary> RunTestClassAsync(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases)
        {
            using (var testClassLifetimeScope =
                _testCollectionLifetimeScope.BeginLifetimeScope(AutofacTestScopes.TestClass,
                                                                builder => builder.RegisterClassFixturesAndModules(testClass, @class)))
            {
                return await new AutofacTestClassRunner(testClassLifetimeScope, testClass, @class, testCases, _diagnosticMessageSink, MessageBus,
                                                        TestCaseOrderer, new ExceptionAggregator(Aggregator), CancellationTokenSource,
                                                        CollectionFixtureMappings).RunAsync();
            }
        }
    }
}
