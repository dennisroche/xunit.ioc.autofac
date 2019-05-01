using System.Collections.Generic;
using System.Threading;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestClassRunnerFactory : IAutofacTestClassRunnerFactory
    {
        public AutofacTestClassRunner Create(IContainer container, ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<AutofacTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus, ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            return new AutofacTestClassRunner(container, testClass, @class, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource);
        }
    }
}
