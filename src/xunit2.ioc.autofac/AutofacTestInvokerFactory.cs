using System;
using System.Reflection;
using System.Threading;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac.TestFramework
{
    public class AutofacTestInvokerFactory : IAutofacTestInvokerFactory
    {
        public AutofacTestInvoker Create(ILifetimeScope container, ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource)
        {
            return new AutofacTestInvoker(container, test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, aggregator, cancellationTokenSource);
        }
    }
}
