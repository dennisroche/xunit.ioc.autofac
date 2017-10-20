using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    internal class AutofacTheoryTestCaseRunner : XunitTheoryTestCaseRunner
    {
        private readonly ILifetimeScope _testClassLifetimeScope;

        public AutofacTheoryTestCaseRunner(IXunitTestCase testCase,
                                           ILifetimeScope testClassLifetimeScope,
                                           string displayName,
                                           string skipReason,
                                           object[] constructorArguments,
                                           IMessageSink diagnosticMessageSink,
                                           IMessageBus messageBus,
                                           ExceptionAggregator aggregator,
                                           CancellationTokenSource cancellationTokenSource)
            : base(testCase, displayName, skipReason, constructorArguments, diagnosticMessageSink, messageBus, aggregator, cancellationTokenSource)
        {
            _testClassLifetimeScope = testClassLifetimeScope;
        }

        protected override XunitTestRunner CreateTestRunner(ITest test,
                                                            IMessageBus messageBus,
                                                            Type testClass,
                                                            object[] constructorArguments,
                                                            MethodInfo testMethod,
                                                            object[] testMethodArguments,
                                                            string skipReason,
                                                            IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes,
                                                            ExceptionAggregator aggregator,
                                                            CancellationTokenSource cancellationTokenSource) => new AutofacTestRunner(
            _testClassLifetimeScope, test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, skipReason, beforeAfterAttributes,
            new ExceptionAggregator(aggregator), cancellationTokenSource);

        protected override ITest CreateTest(IXunitTestCase testCase, string displayName) => new AutofacTest(_testClassLifetimeScope, testCase, displayName);
    }
}
