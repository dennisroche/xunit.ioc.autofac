using System.Collections.Generic;
using System.Threading;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestCollectionRunnerFactory : IAutofacTestCollectionRunnerFactory
    {
        public AutofacTestCollectionRunner Create(IContainer container, ITestCollection testCollection, IEnumerable<AutofacTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus, ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            return new AutofacTestCollectionRunner(container, testCollection, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource);
        }
    }
}
