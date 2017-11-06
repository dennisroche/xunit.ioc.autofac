using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    internal class AutofacSkippedDataRowTestCase : AutofacTestCase
    {
        private string _skipReason;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public AutofacSkippedDataRowTestCase() { }

        public AutofacSkippedDataRowTestCase(IMessageSink diagnosticMessageSink,
                                             TestMethodDisplay defaultMethodDisplay,
                                             ITestMethod testMethod,
                                             string skipReason,
                                             object[] testMethodArguments = null)
            :
            base(diagnosticMessageSink, defaultMethodDisplay, testMethod, testMethodArguments)
        {
            _skipReason = skipReason;
        }

        public override void Deserialize(IXunitSerializationInfo data)
        {
            base.Deserialize(data);

            _skipReason = data.GetValue<string>("SkipReason");
        }

        /// <inheritdoc />
        protected override string GetSkipReason(IAttributeInfo factAttribute) => _skipReason;

        /// <inheritdoc />
        public override void Serialize(IXunitSerializationInfo data)
        {
            base.Serialize(data);

            data.AddValue("SkipReason", _skipReason);
        }

        public override async Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink,
                                                        IMessageBus messageBus,
                                                        object[] constructorArguments,
                                                        ExceptionAggregator aggregator,
                                                        CancellationTokenSource cancellationTokenSource) => await new AutofacTheoryTestCaseRunner(
            this, TestClassLifetimeScope, DisplayName, SkipReason, constructorArguments, diagnosticMessageSink,
            messageBus,
            aggregator, cancellationTokenSource).RunAsync();
    }
}
