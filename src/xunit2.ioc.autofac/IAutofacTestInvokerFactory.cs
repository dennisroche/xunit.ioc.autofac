using System;
using System.Reflection;
using System.Threading;
using Autofac;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Ioc.Autofac
{
    public interface IAutofacTestInvokerFactory
    {
        AutofacTestInvoker Create(ILifetimeScope container, ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource);
    }
}
