using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    internal class AutofacTheoryTestCase : AutofacTestCase
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public AutofacTheoryTestCase() { }

        public AutofacTheoryTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod, object[] testMethodArguments = null)
            : base(diagnosticMessageSink, defaultMethodDisplay, testMethod, testMethodArguments) { }

        public override Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink,
                                                  IMessageBus messageBus,
                                                  object[] constructorArguments,
                                                  ExceptionAggregator aggregator,
                                                  CancellationTokenSource cancellationTokenSource) =>
            new AutofacTheoryTestCaseRunner(this, TestClassLifetimeScope, DisplayName, SkipReason, constructorArguments, diagnosticMessageSink, messageBus,
                                            aggregator, cancellationTokenSource).RunAsync();
    }
}
