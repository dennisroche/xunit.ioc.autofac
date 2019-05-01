using System;
using System.Reflection;
using System.Threading;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestRunnerFactory : IAutofacTestRunnerFactory
    {
        public AutofacTestRunner Create(IContainer container, ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, string skipReason, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            return new AutofacTestRunner(container, test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, skipReason, aggregator, cancellationTokenSource);
        }
    }
}
