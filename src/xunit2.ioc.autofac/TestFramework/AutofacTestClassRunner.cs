using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestClassRunner : XunitTestClassRunner
    {
        private readonly ILifetimeScope _testClassLifetimeScope;

        public AutofacTestClassRunner(ILifetimeScope testClassLifetimeScope,
                                      ITestClass testClass,
                                      IReflectionTypeInfo @class,
                                      IEnumerable<IXunitTestCase> testCases,
                                      IMessageSink diagnosticMessageSink,
                                      IMessageBus messageBus,
                                      ITestCaseOrderer testCaseOrderer,
                                      ExceptionAggregator aggregator,
                                      CancellationTokenSource cancellationTokenSource,
                                      Dictionary<Type, object> collectionFixtureMappings)
            : base(testClass, @class, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource,
                   collectionFixtureMappings)
        {
            _testClassLifetimeScope = testClassLifetimeScope;
        }

        protected override void CreateClassFixture(Type fixtureType)
        {
            Aggregator.Run(() => { ClassFixtureMappings[fixtureType] = _testClassLifetimeScope.Resolve(fixtureType); });
        }

        protected override async Task<RunSummary> RunTestMethodAsync(ITestMethod testMethod,
                                                                     IReflectionMethodInfo method,
                                                                     IEnumerable<IXunitTestCase> testCases,
                                                                     object[] constructorArguments) =>
            await new AutofacTestMethodRunner(_testClassLifetimeScope, DiagnosticMessageSink, testMethod, Class, method, testCases, MessageBus,
                                              new ExceptionAggregator(Aggregator), CancellationTokenSource, constructorArguments).RunAsync();

        protected override object[] CreateTestClassConstructorArguments() => new object[0];
    }
}
