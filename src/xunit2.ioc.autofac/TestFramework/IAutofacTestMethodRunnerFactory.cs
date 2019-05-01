using System.Collections.Generic;
using System.Threading;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public interface IAutofacTestMethodRunnerFactory
    {
        AutofacTestMethodRunner Create(IContainer container, IMessageSink diagnosticMessageSink, ITestMethod testMethod, IReflectionTypeInfo @class, IReflectionMethodInfo method, IEnumerable<AutofacTestCase> testCases, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource);
    }
}
